using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models.OpenJK
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 532, CharSet = CharSet.Ansi)]
    public struct EntityState
    {
        public int Number;

        public int EType;

        public int EFlags;

        public int EFlags2;

        public Trajectory Pos;

        public Trajectory APos;

        public int Time;

        public int Time2;

        public Vector3 Origin;

        public Vector3 Origin2;

        public Vector3 Angles;

        public Vector3 Angles2;

        public int Bolt1;

        public int Bolt2;

        public int TrickedEntIndex;

        public int TrickedEntIndex2;

        public int TrickedEntIndex3;

        public int TrickedEntIndex4;

        public float Speed;

        public int FireFlag;

        public int GenericEnemyIndex;

        public int ActiveForcePass;

        public int EmplacedOwner;

        public int OtherEntityNum;

        public int OtherEntityNum2;

        public int GroundEntityNum;

        public int ConstantLight;

        public int LoopSound;

        public int LoopIsSoundSet;

        public int SoundSetIndex;

        public int ModelGhoul2;

        public int G2Radius;

        public int ModelIndex;

        public int ModelIndex2;

        public int ClientNum;

        public int Frame;

        public int SaberInFlight;

        public int SaberEntityNum;

        public int SaberMove;

        public int ForcePowersActive;

        public int SaberHolstered;

        public int IsJediMaster;

        public int IsPortalEnt;

        public int Solid;

        public int Event;

        public int EventParm;

        public int Owner;

        public int TeamOwner;

        public int ShouldTarget;

        public int PowerUps;

        public int Weapon;

        public int LegsAnim;

        public int TorsoAnim;

        public int LegsFlip;

        public int TorsoFlip;

        public int ForceFrame;

        public int Generic1;

        public int HeldByClient;

        public int RagAttach;

        public int iModelScale;

        public int BrokenLimbs;

        public int BoltToPlayer;

        public int HasLookTarget;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I4)]
        public int[] CustomRGBA;

        public int Health;

        public int MaxHealth;

        public int NpcSaber1;

        public int NpcSaber2;

        public int CsSoundsStd;

        public int CsSoundsCombat;

        public int CsSounds_Extra;

        public int CsSounds_Jedi;

        public int SurfacesOn;

        public int SurfacesOff;

        public int BoneIndex1;

        public int BoneIndex2;

        public int BoneIndex3;

        public int BoneIndex4;

        public int BoneOrient;

        public Vector3 BoneAngles1;

        public Vector3 BoneAngles2;

        public Vector3 BoneAngles3;

        public Vector3 BoneAngles4;

        public int NPCClass;

        public int MIVehicleNum;

        public int UserInt1;

        public int UserInt2;

        public int UserInt3;

        public float UserFloat1;

        public float UserFloat2;

        public float UserFloat3;

        public Vector3 UserVec1;

        public Vector3 UserVec2;
       
    }
}
