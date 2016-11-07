using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace RosettaToRevit
{
    [ProtoContract]
    public struct Name
    {
        [ProtoMember(1)]
        public string name;
    }

    [ProtoContract]
    public struct Box
    {
        [ProtoMember(1)]
        public float p0coordx;
        [ProtoMember(2)]
        public float p0coordy;
        [ProtoMember(3)]
        public float p0coordz;
        [ProtoMember(4)]
        public float p1coordx;
        [ProtoMember(5)]
        public float p1coordy;
        [ProtoMember(6)]
        public float p1coordz;
        [ProtoMember(7)]
        public float p2coordx;
        [ProtoMember(8)]
        public float p2coordy;
        [ProtoMember(9)]
        public float p2coordz;
        [ProtoMember(10)]
        public float p3coordx;
        [ProtoMember(11)]
        public float p3coordy;
        [ProtoMember(12)]
        public float p3coordz;
        [ProtoMember(13)]
        public float height;
    }

    [ProtoContract]
    public struct Cylinder
    {
        [ProtoMember(1)]
        public float p0coordx;
        [ProtoMember(2)]
        public float p0coordy;
        [ProtoMember(3)]
        public float p0coordz;
        [ProtoMember(4)]
        public float radius;
        [ProtoMember(5)]
        public float height;
    }

    [ProtoContract]
    public struct Cylinderb
    {
        [ProtoMember(1)]
        public float p0coordx;
        [ProtoMember(2)]
        public float p0coordy;
        [ProtoMember(3)]
        public float p0coordz;
        [ProtoMember(4)]
        public float radius;
        [ProtoMember(5)]
        public float p1coordx;
        [ProtoMember(6)]
        public float p1coordy;
        [ProtoMember(7)]
        public float p1coordz;
    }

    [ProtoContract]
    public struct Sphere
    {
        [ProtoMember(1)]
        public float p0coordx;
        [ProtoMember(2)]
        public float p0coordy;
        [ProtoMember(3)]
        public float p0coordz;
        [ProtoMember(4)]
        public float p1coordx;
        [ProtoMember(5)]
        public float p1coordy;
        [ProtoMember(6)]
        public float p1coordz;
        [ProtoMember(7)]
        public float p2coordx;
        [ProtoMember(8)]
        public float p2coordy;
        [ProtoMember(9)]
        public float p2coordz;
    }
    class ProtoContracts
    {
    }
}
