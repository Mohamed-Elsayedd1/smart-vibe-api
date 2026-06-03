using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBayt.Data;
using SmartBayt.DTOs;
using SmartBayt.Helpers;
using SmartBayt.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using RegisterRequest = SmartBayt.DTOs.RegisterRequest;
using LoginRequest = SmartBayt.DTOs.LoginRequest;
using ForgotPasswordRequest = SmartBayt.DTOs.ForgotPasswordRequest;
using ResetPasswordRequest = SmartBayt.DTOs.ResetPasswordRequest;

namespace SmartBayt.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AppDbContext db, JwtHelper jwt) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest req)
    {
        if (await db.Users.AnyAsync(u => u.Email == req.Email))
            return BadRequest(new { message = "البريد الإلكتروني مستخدم بالفعل" });

        var user = new AppUser
        {
            Email = req.Email.ToLower().Trim(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
            FullName = req.FullName,
            Role = "user"
        };
        db.Users.Add(user);
        await db.SaveChangesAsync();

        return Ok(new AuthResponse(jwt.GenerateToken(user), user.Id.ToString(), user.Email, user.FullName, user.Role));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == req.Email.ToLower().Trim());
        if (user is null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            return Unauthorized(new { message = "بيانات خاطئة" });

        return Ok(new AuthResponse(jwt.GenerateToken(user), user.Id.ToString(), user.Email, user.FullName, user.Role));
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await db.Users.FindAsync(id);
        if (user is null) return NotFound();
        return Ok(new AuthResponse("", user.Id.ToString(), user.Email, user.FullName, user.Role));
    }

    [HttpPut("profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest req)
    {
        var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await db.Users.FindAsync(id);
        if (user is null) return NotFound();

        if (!string.IsNullOrWhiteSpace(req.FullName))
            user.FullName = req.FullName;

        if (!string.IsNullOrWhiteSpace(req.AvatarUrl))
            user.AvatarUrl = req.AvatarUrl;

        if (!string.IsNullOrWhiteSpace(req.NewPassword))
        {
            if (string.IsNullOrWhiteSpace(req.CurrentPassword))
                return BadRequest(new { message = "كلمة المرور الحالية مطلوبة" });

            if (!BCrypt.Net.BCrypt.Verify(req.CurrentPassword, user.PasswordHash))
                return BadRequest(new { message = "كلمة المرور الحالية غير صحيحة" });

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);
        }

        await db.SaveChangesAsync();
        return Ok(new { message = "تم التحديث", fullName = user.FullName, avatarUrl = user.AvatarUrl });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest req)
    {
        // دايما نرجع نفس الرسالة عشان ما نكشفش إيه البريد المسجل
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == req.Email.ToLower().Trim());
        if (user is null)
            return Ok(new { message = "تم إرسال كود التحقق إذا كان البريد مسجلاً" });

        var code = new Random().Next(100000, 999999).ToString();
        user.ResetCode = code;
        user.ResetCodeExpiry = DateTime.UtcNow.AddMinutes(15);
        await db.SaveChangesAsync();

        // ─── إرسال الإيميل ─────────────────────────────────────
        // TODO: فعّل خدمة الإيميل هنا — مثال باستخدام MailKit/SMTP:
        //
        // using var smtp = new MailKit.Net.Smtp.SmtpClient();
        // await smtp.ConnectAsync(config["Email:Host"], 587, false);
        // await smtp.AuthenticateAsync(config["Email:User"], config["Email:Pass"]);
        // var mail = new MimeKit.MimeMessage();
        // mail.From.Add(MimeKit.InternetAddress.Parse("noreply@smartbayt.com"));
        // mail.To.Add(MimeKit.InternetAddress.Parse(user.Email));
        // mail.Subject = "كود إعادة تعيين كلمة المرور";
        // mail.Body = new MimeKit.TextPart("plain") { Text = $"الكود: {code}\nصالح لمدة 15 دقيقة." };
        // await smtp.SendAsync(mail);
        // await smtp.DisconnectAsync(true);
        //
        // في appsettings.json أضف:
        // "Email": { "Host": "smtp.gmail.com", "User": "...", "Pass": "..." }
        // ─────────────────────────────────────────────────────────

        return Ok(new { message = "تم إرسال كود التحقق إذا كان البريد مسجلاً" });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest req)
    {
        var user = await db.Users.FirstOrDefaultAsync(u =>
            u.Email == req.Email.ToLower().Trim() &&
            u.ResetCode == req.Code &&
            u.ResetCodeExpiry > DateTime.UtcNow);

        if (user is null) return BadRequest(new { message = "الكود غير صحيح أو منتهي الصلاحية" });

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);
        user.ResetCode = null;
        user.ResetCodeExpiry = null;
        await db.SaveChangesAsync();

        return Ok(new { message = "تم تغيير كلمة المرور بنجاح" });
    }

    // ─── Admin: قائمة المستخدمين ──────────────────────────
    [HttpGet("users")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetUsers() =>
        Ok(await db.Users.OrderByDescending(u => u.CreatedAt)
            .Select(u => new { u.Id, u.Email, u.FullName, u.Role, u.CreatedAt })
            .ToListAsync());
}
