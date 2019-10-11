﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoliX;

namespace PoliX.Triangulation
{
    public class Arc //Ребро
    {
        public Node A;
        public Node B;

        //Ребро является границей триангуляции если не ссылается на 2 треугольника
        public bool isBorder = true;

        public Vector2 AB;

        //Ссылка на треугольники в которые входит ребро
        public Triangle trAB;
        public Triangle trBA;

        public double sqrMagnitude;

        public Arc()
        { }

        public Arc(Node _A, Node _B)
        {
            A = _A;
            B = _B;

            //Добавление ссылок на ребро в узлах
            A.arcs.Add(this);
            B.arcs.Add(this);

            AB = A.point - B.point;

            
            sqrMagnitude = AB.x * AB.x + AB.y * AB.y; 
        }

        public static bool ArcIntersect(Arc a1, Arc a2)
        {
            Vector2 p1, p2, p3, p4;
            p1 = a1.A.point;
            p2 = a1.B.point;
            p3 = a2.A.point;
            p4 = a2.B.point;

            double d1 = Direction(p3, p4, p1);
            double d2 = Direction(p3, p4, p2);
            double d3 = Direction(p1, p2, p3);
            double d4 = Direction(p1, p2, p4);

            if (p1 == p3 || p1 == p4 || p2 == p3 || p2 == p4)
                return false;
            else if (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) &
                     ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0)))
                return true;
            else if ((d1 == 0) && OnSegment(p3, p4, p1))
                return true;
            else if ((d2 == 0) && OnSegment(p3, p4, p2))
                return true;
            else if ((d3 == 0) && OnSegment(p1, p2, p3))
                return true;
            else if ((d4 == 0) && OnSegment(p1, p2, p4))
                return true;
            else
                return false;
        }

        public Node GetSecondNode(Node _firstnode)
        {
            if (A == _firstnode)
                return A;
            else if (B == _firstnode)
                return B;
            else
                return null;
        }

        private static double Direction(Vector2 pi, Vector2 pj, Vector2 pk)
        {
            return Vector2.crossProduct((pk - pi), (pj - pi));
        }
        private static bool OnSegment(Vector2 pi, Vector2 pj, Vector2 pk)
        {
            if ((Math.Min(pi.x, pj.x) <= pk.x && pk.x <= Math.Max(pi.x, pj.x)) && (Math.Min(pi.y, pj.y) <= pk.y && pk.y <= Math.Max(pi.y, pj.y)))
                return true;
            else
                return false;
        }



    }
}
