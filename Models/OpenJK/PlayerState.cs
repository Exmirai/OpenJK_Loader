using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models.OpenJK
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 1552, CharSet = CharSet.Ansi)]
    public struct PlayerState
    {
        public int CommandTime;

        public int PmType;

        public int BobCycle;

        public int PmFlags;

        public int PmTime;

        public Vector3 Origin;

        public Vector3 Velocity;

        public Vector3 MoveDir;

        public int WeaponTime;

        public int WeaponChargeTime;

        public int WeaponChargeSubtractTime;

        public int Gravity;

        public float Speed;

        public int BaseSpeed;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I4)]
        public int[] DeltaAngles;

        public int SlopeRecalcTime;

        public int UseTime;

        public int GroundEntityNum;

        public int LegsTimer;

        public int LegsAnim;

        public int TorsoTimer;

        public int TorsoAnim;

        public int LegsFlip;

        public int TorsoFlip;

        public int MovementDir;

        public int EFlags;

        public int EFLags2;

        public int EventSequence;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I4)]
        public int[] Events;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I4)]
        public int[] EventParams;

        public int ExternalEvent;

        public int ExternalEventParm;

        public int ExternalEventTime;

        public int ClientNum;

        public int Weapon;

        public int WeaponState;

        public Vector3 ViewAngles;

        public int ViewHeight;

        public int DamageEvent;

        public int DamageYaw;

        public int DamagePitch;

        public int DamageCount;

        public int DamageType;

        public int PainType;

        public int PainDirection;

        public float YawAngle;

        public int Yawing;

        public float PitchAngles;

        public int Pitching;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I4)]
        public int[] Stats;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I4)]
        public int[] Persistant;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I4)]
        public int[] PowerUps;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19, ArraySubType = UnmanagedType.I4)]
        public int[] Ammo;

        public int Generic1;

        public int LoopSound;

        public int JumpPadEnt;

        public int Ping;

        public int PMove_FrameCount;

        public int JumpPad_Frame;

        public int EntityEventSequence;

        public int LastOnGround;

        public int SaberInFlight;

        public int SaberMove;

        public int SaberBlocking;

        public int SaberBlocked;

        public int SaberLockTime;

        public int SaberLockEnemy;

        public int SaberLockFrame;

        public int SaberLockHits;

        public int SaberLockHitCheckTime;

        public int SaberLockHitIncrementTime;

        public int SaberLockAdvance;

        public int SaberEntityNum;

        public int SaberEntityDist;

        public int SaberEntityState;

        public int SaberThrowDelay;

        public int SaberCanThrow;

        public int SaberDidThrowTime;

        public int SaberDamangeDebounceTime;

        public int SaberHitWallSoundDebounceTime;

        public int SaberEventFlags;


        public int RocketLockIndex;

        public float RocketLastValidTime;

        public float RocketLockTime;

        public float RocketTargetTime;


        public int EmplacedIndex;

        public float EmplacedTime;


        public int IsJediMaster;

        public int ForceRestricted;

        public int TrueJedi;

        public int TrueNonJedi;

        public int SaberIndex;


        public int GenericEnemyIndex;

        public float DroneFireTime;

        public float DroneExistsTime;

        public int ActiveForcePass;

        public int HasDetPackPlanted;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.R4)]
        public float[] HolocronsCarried;

        public int HolocronCantTouch;

        public float HolocronCantTouchTime;

        public int HolocronBits;

        public int ElectrifyTime;

        public int SaberAttackSequence;

        public int SaberIdleWound;

        public int SaberAttackWound;

        public int SaberBlockTime;


        public int OtherKiller;

        public int OtherKillerTime;

        public int OtherKillerDebounceTime;

        public ForceData ForceData;

        public int ForceJumpFlip;

        public int ForceHandExtend;

        public int ForceHandExtendTime;

        public int ForceRageDrainTime;

        public int ForceDodgeAnim;

        public int QuickerGetup;

        public int GroundTime;

        public int FootstepTime;

        public int OtherSoundTime;

        public int OtherSoundLen;

        public int ForceGripMoveInterval;

        public int ForceGripChangeMoveType;

        public int ForceKickFlip;

        public int DuelIndex;

        public int DuelTime;

        public int DuelInProgress;

        public int SaberAttackChainCount;

        public int SaberHolstered;

        public int ForceAllowDeactivateTime;

        public int ZoomMode;

        public int ZoomTime;

        public int ZoomLocked;

        public int ZoomFov;

        public int ZoomLockTime;

        public int FallingToDeath;

        public int UseDelay;

        public int InAirAnim;

        public Vector3 LastHitLoc;

        public int HeldByClient;

        public int RagAttach;

        public int iModelScale;

        public int BrokenLimbs;

        public int HasLookTarget;

        public int LookTarget;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I4)]
        public int[] CustomRGBA;

        public int StandHeight;

        public int CrouchHeight;

        public int MIVehicleNum;

        public Vector3 VehOrientation;

        public int VehBoarding;

        public int VehSurfaces;

        public int VehTurnAroundIndex;

        public int VehTurnAroundTime;

        public int VehWeaponsLinked;

        public int HyperSpaceTime;

        public Vector3 HyperSpaceAngles;

        public int HackingBaseTime;

        public int JetPackFuel;

        public int ClockFuel;

        public int UserInt1;

        public int UserInt2;

        public float UserFloat1;

        public float UserFloat2;

        public float UserFloat3;

        public Vector3 UserVec1;

        public Vector3 UserVec2;
    }
}
