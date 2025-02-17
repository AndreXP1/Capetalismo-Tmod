using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CapetalismoTmod.Content.Items.Weapons
{
    public class ThrowingKnifeProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 50;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = ProjAIStyleID.Boomerang;
            Projectile.friendly = true;
            AIType = ProjectileID.EnchantedBoomerang;
            Projectile.tileCollide = true;
            Projectile.penetrate = 5;


        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
            }

            Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Projectile.spriteDirection == 1)
            {
                Projectile.rotation += MathHelper.PiOver2;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate -= 1;

            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }

            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }

            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }
    }
}
