
using System;
using CapetalismoTmod.Content.Items.Projectiles.Minions;
using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CapetalismoTmod.Content.Items.Projectiles.Minions{
public class RoncaTussaProjectile : ModProjectile
	{

				public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 4;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

			// This code is needed to customize the vanity pet display in the player select screen. Quick explanation:
			// * It uses fluent API syntax, just like Recipe
			// * You start with ProjectileID.Sets.SimpleLoop, specifying the start and end frames as well as the speed, and optionally if it should animate from the end after reaching the end, effectively "bouncing"
			// * To stop the animation if the player is not highlighted/is standing, as done by most grounded pets, add a .WhenNotSelected(0, 0) (you can customize it just like SimpleLoop)
			// * To set offset and direction, use .WithOffset(x, y) and .WithSpriteDirection(-1)
			// * To further customize the behavior and animation of the pet (as its AI does not run), you have access to a few vanilla presets in DelegateMethods.CharacterPreview to use via .WithCode(). You can also make your own, showcased in MinionBossPetProjectile
		}

		public override void SetDefaults() {
			AIType = ProjectileID.ZephyrFish; // Mimic as the Zephyr Fish during AI.

			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.damage = 50;
			Projectile.minion = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.knockBack = 1f;
			Projectile.penetrate = -1;
		}
		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];

			player.zephyrfish = false; // Relic from AIType

			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];

			// Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
			if (!player.dead) {
				Projectile.Kill();
				return;
			}
			if (Projectile.localAI[0] == 0f){
              // Define a posição de início do pet, indo até perto do jogador
              Projectile.Center = player.Center;
              Projectile.localAI[0] = 1f;
          }
					Vector2 targetPosition = player.Center;
					float speed = 7f;
					float distance = Vector2.Distance(Projectile.Center, targetPosition);
					if(distance > 10f){
						Vector2 direction = targetPosition - Projectile.Center;
						direction.Normalize();
						Projectile.velocity = direction * speed;
					}
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(target.active && !target.friendly && Projectile.Hitbox.Intersects(target.Hitbox)){
							target.StrikeNPC(hit);
						}
        }


        // 	public override void SetStaticDefaults() {
        // 		// Sets the amount of frames this minion has on its spritesheet
        // 		Main.projFrames[Projectile.type] = 4;
        // 		// This is necessary for right-click targeting
        // 		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

        // 		Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

        // 		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
        // 		ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        // 	}

        // 	public sealed override void SetDefaults() {
        // 		Projectile.width = 18;
        // 		Projectile.height = 28;
        // 		Projectile.tileCollide = false; // Makes the minion go through tiles freely

        // 		// These below are needed for a minion weapon
        // 		Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
        // 		Projectile.minion = true; // Declares this as a minion (has many effects)
        // 		Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
        // 		Projectile.minionSlots = 1f; // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
        // 		Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles
        // 	}

        // 	// Here you can decide if your minion breaks things like grass or pots
        // 	public override bool? CanCutTiles() {
        // 		return false;
        // 	}

        // 	// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        // 	public override bool MinionContactDamage() {
        // 		return true;
        // 	}

        // 	// The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
        // 	public override void AI() {
        // 		Player owner = Main.player[Projectile.owner];

        // 		if (!CheckActive(owner)) {
        // 			return;
        // 		}

        // 		GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
        // 		SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
        // 		Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
        // 		Visuals();
        // 	}

        // 	// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        // 	private bool CheckActive(Player owner) {
        // 		if (owner.dead || !owner.active) {
        // 			owner.ClearBuff(ModContent.BuffType<RoncaTussaBuff>());

        // 			return false;
        // 		}

        // 		if (owner.HasBuff(ModContent.BuffType<RoncaTussaBuff>())) {
        // 			Projectile.timeLeft = 2;
        // 		}

        // 		return true;
        // 	}

        // 	private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition) {
        // 		Vector2 idlePosition = owner.Center;
        // 		idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

        // 		// If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
        // 		// The index is projectile.minionPos
        // 		float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
        // 		idlePosition.X += minionPositionOffsetX; // Go behind the player

        // 		// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

        // 		// Teleport to player if distance is too big
        // 		vectorToIdlePosition = idlePosition - Projectile.Center;
        // 		distanceToIdlePosition = vectorToIdlePosition.Length();

        // 		if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f) {
        // 			// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
        // 			// and then set netUpdate to true
        // 			Projectile.position = idlePosition;
        // 			Projectile.velocity *= 0.1f;
        // 			Projectile.netUpdate = true;
        // 		}

        // 		// If your minion is flying, you want to do this independently of any conditions
        // 		float overlapVelocity = 0.04f;

        // 		// Fix overlap with other minions
        // 		foreach (var other in Main.ActiveProjectiles) {
        // 			if (other.whoAmI != Projectile.whoAmI && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width) {
        // 				if (Projectile.position.X < other.position.X) {
        // 					Projectile.velocity.X -= overlapVelocity;
        // 				}
        // 				else {
        // 					Projectile.velocity.X += overlapVelocity;
        // 				}

        // 				if (Projectile.position.Y < other.position.Y) {
        // 					Projectile.velocity.Y -= overlapVelocity;
        // 				}
        // 				else {
        // 					Projectile.velocity.Y += overlapVelocity;
        // 				}
        // 			}
        // 		}
        // 	}

        // 	private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter) {
        // 		// Starting search distance
        // 		distanceFromTarget = 700f;
        // 		targetCenter = Projectile.position;
        // 		foundTarget = false;

        // 		// This code is required if your minion weapon has the targeting feature
        // 		if (owner.HasMinionAttackTargetNPC) {
        // 			NPC npc = Main.npc[owner.MinionAttackTargetNPC];
        // 			float between = Vector2.Distance(npc.Center, Projectile.Center);

        // 			// Reasonable distance away so it doesn't target across multiple screens
        // 			if (between < 2000f) {
        // 				distanceFromTarget = between;
        // 				targetCenter = npc.Center;
        // 				foundTarget = true;
        // 			}
        // 		}

        // 		if (!foundTarget) {
        // 			// This code is required either way, used for finding a target
        // 			foreach (var npc in Main.ActiveNPCs) {
        // 				if (npc.CanBeChasedBy()) {
        // 					float between = Vector2.Distance(npc.Center, Projectile.Center);
        // 					bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
        // 					bool inRange = between < distanceFromTarget;
        // 					bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
        // 					// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
        // 					// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
        // 					bool closeThroughWall = between < 100f;

        // 					if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall)) {
        // 						distanceFromTarget = between;
        // 						targetCenter = npc.Center;
        // 						foundTarget = true;
        // 					}
        // 				}
        // 			}
        // 		}

        // 		// friendly needs to be set to true so the minion can deal contact damage
        // 		// friendly needs to be set to false so it doesn't damage things like target dummies while idling
        // 		// Both things depend on if it has a target or not, so it's just one assignment here
        // 		// You don't need this assignment if your minion is shooting things instead of dealing contact damage
        // 		Projectile.friendly = foundTarget;
        // 	}

        // 	private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition) {
        // 		// Default movement parameters (here for attacking)
        // 		float speed = 8f;
        // 		float inertia = 20f;

        // 		if (foundTarget) {
        // 			// Minion has a target: attack (here, fly towards the enemy)
        // 			if (distanceFromTarget > 40f) {
        // 				// The immediate range around the target (so it doesn't latch onto it when close)
        // 				Vector2 direction = targetCenter - Projectile.Center;
        // 				direction.Normalize();
        // 				direction *= speed;

        // 				Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
        // 			}
        // 		}
        // 		else {
        // 			// Minion doesn't have a target: return to player and idle
        // 			if (distanceToIdlePosition > 600f) {
        // 				// Speed up the minion if it's away from the player
        // 				speed = 12f;
        // 				inertia = 60f;
        // 			}
        // 			else {
        // 				// Slow down the minion if closer to the player
        // 				speed = 4f;
        // 				inertia = 80f;
        // 			}

        // 			if (distanceToIdlePosition > 20f) {
        // 				// The immediate range around the player (when it passively floats about)

        // 				// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
        // 				vectorToIdlePosition.Normalize();
        // 				vectorToIdlePosition *= speed;
        // 				Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
        // 			}
        // 			else if (Projectile.velocity == Vector2.Zero) {
        // 				// If there is a case where it's not moving at all, give it a little "poke"
        // 				Projectile.velocity.X = -0.15f;
        // 				Projectile.velocity.Y = -0.05f;
        // 			}
        // 		}
        // 	}

        // 	private void Visuals() {
        // 		// So it will lean slightly towards the direction it's moving
        // 		Projectile.rotation = Projectile.velocity.X * 0.05f;

        // 		// This is a simple "loop through all frames from top to bottom" animation
        // 		int frameSpeed = 5;

        // 		Projectile.frameCounter++;

        // 		if (Projectile.frameCounter >= frameSpeed) {
        // 			Projectile.frameCounter = 0;
        // 			Projectile.frame++;

        // 			if (Projectile.frame >= Main.projFrames[Projectile.type]) {
        // 				Projectile.frame = 0;
        // 			}
        // 		}

        // 		// Some visuals here
        // 		Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
        // 	}
    }
}