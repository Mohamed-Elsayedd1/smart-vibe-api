# إعداد Environment Variables

## التطوير المحلي (Development)

أضف الـ secrets في `appsettings.Development.json` (مش بيتحمل على Git):

```json
{
  "Jwt": {
    "Secret": "ضع هنا secret قوي لا يقل عن 32 حرف"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=smartbayt;Username=postgres;Password=..."
  }
}
```

## الإنتاج على Railway

أضف هذه المتغيرات في Railway Dashboard → Variables:

| اسم المتغير | القيمة |
|---|---|
| `JWT_SECRET` | secret قوي عشوائي (32+ حرف) |
| `FRONTEND_URL` | رابط الـ frontend مثلاً https://smartbayt.vercel.app |
| `DATABASE_URL` | بييجي أوتوماتيك من Railway PostgreSQL |

## توليد JWT Secret قوي

```bash
# في terminal:
openssl rand -base64 32
```
