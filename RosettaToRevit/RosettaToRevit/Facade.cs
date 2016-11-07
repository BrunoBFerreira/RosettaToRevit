using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.Creation;

namespace RosettaToRevit
{

    class Facade
    {
        private UIApplication uiapp = null;

        private int levelCounter = 2;

        private int customFamilyCounter = 0;

       public Facade(UIApplication app)
        {
            uiapp = app;
        }

       public void TestFunc()
       {
           TaskDialog.Show("Revit", "Parabens! Encontrou um segredo!");
       }

        public int DrawBox(double P0Coordx, double P0Coordy, double P0Coordz, 
                            double P1Coordx, double P1Coordy, double P1Coordz, 
                            double P2Coordx, double P2Coordy, double P2Coordz, 
                            double P3Coordx, double P3Coordy, double P3Coordz, 
                            double h)
        {
            
                using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a cube"))
                {
                    t.Start();
                    /*
                    XYZ verticeA = new XYZ(P0Coordx, P0Coordy, P0Coordz);
                    XYZ verticeB = new XYZ(P1Coordx, P1Coordy, P1Coordz);
                    XYZ verticeC = new XYZ(P2Coordx, P2Coordy, P2Coordz);
                    XYZ verticeD = new XYZ(P3Coordx, P3Coordy, P3Coordz);

                    CurveArrArray cubeProfile = new CurveArrArray();
                    CurveArray cubeBase = new CurveArray();

                    Line sideA = Line.CreateBound(verticeA, verticeB);
                    Line sideB = Line.CreateBound(verticeB, verticeC);
                    Line sideC = Line.CreateBound(verticeC, verticeD);
                    Line sideD = Line.CreateBound(verticeD, verticeA);

                    cubeBase.Append(sideA);
                    cubeBase.Append(sideB);
                    cubeBase.Append(sideC);
                    cubeBase.Append(sideD);

                    cubeProfile.Append(cubeBase);

                    XYZ normal = new XYZ(0, 0, 1);
                    XYZ origin = new XYZ(0, 0, verticeA.Z);
                    Plane pl = new Plane(normal, origin);

                    SketchPlane sp = SketchPlane.Create(uiapp.ActiveUIDocument.Document, pl);

                    Extrusion cubeEX = uiapp.ActiveUIDocument.Document.FamilyCreate.NewExtrusion(true, cubeProfile, sp, h);
                                 
                    return cubeEX.Id.IntegerValue;

                    */
                    
                    FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                    failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                    t.SetFailureHandlingOptions(failOp);

                    XYZ verticeA = new XYZ(P0Coordx, P0Coordy, P0Coordz);
                    XYZ verticeB = new XYZ(P1Coordx, P1Coordy, P1Coordz);
                    XYZ verticeC = new XYZ(P2Coordx, P2Coordy, P2Coordz);
                    XYZ verticeD = new XYZ(P3Coordx, P3Coordy, P3Coordz);

                    ReferenceArray refAr = new ReferenceArray();

                    Line sideA = Line.CreateBound(verticeA, verticeB);
                    Line sideB = Line.CreateBound(verticeB, verticeC);
                    Line sideC = Line.CreateBound(verticeC, verticeD);
                    Line sideD = Line.CreateBound(verticeD, verticeA);

                    XYZ normal = new XYZ(0, 0, 1);
                    XYZ origin = new XYZ(0, 0, verticeA.Z);
                    Plane pl = new Plane(normal, origin);

                    SketchPlane sp = SketchPlane.Create(uiapp.ActiveUIDocument.Document, pl);

                    ModelLine line1 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewModelCurve(sideA, sp) as ModelLine;
                    ModelLine line2 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewModelCurve(sideB, sp) as ModelLine;
                    ModelLine line3 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewModelCurve(sideC, sp) as ModelLine;
                    ModelLine line4 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewModelCurve(sideD, sp) as ModelLine;


                    refAr.Append(line1.GeometryCurve.Reference);
                    refAr.Append(line2.GeometryCurve.Reference);
                    refAr.Append(line3.GeometryCurve.Reference);
                    refAr.Append(line4.GeometryCurve.Reference);

                    XYZ direction = new XYZ(0, 0, h);

                    Form cubeEX = uiapp.ActiveUIDocument.Document.FamilyCreate.NewExtrusionForm(true, refAr, direction);

                    t.Commit();

                    return cubeEX.Id.IntegerValue;
                }

        }



        public IList<int> DrawSphere(double P0Coordx, double P0Coordy, double P0Coordz, 
                               double P1Coordx, double P1Coordy, double P1Coordz, 
                               double P2Coordx, double P2Coordy, double P2Coordz)
        {
           
                using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a sphere"))
                {
                    t.Start();
                    /*
                    XYZ begin = new XYZ(P0Coordx, P0Coordy, P0Coordz);
                    XYZ end = new XYZ(P1Coordx, P1Coordy, P1Coordz);
                    XYZ top = new XYZ(P2Coordx, P2Coordy, P2Coordz);

                    Arc arc = Arc.Create(begin, end, top);

                    Line line = Line.CreateBound(arc.GetEndPoint(1), arc.GetEndPoint(0));

                    CurveArray halfSphereBase = new CurveArray();
                    halfSphereBase.Append(arc);
                    halfSphereBase.Append(line);

                    CurveArrArray sphereProfile = new CurveArrArray();
                    sphereProfile.Append(halfSphereBase);

                    XYZ normal = new XYZ(0, 0, 1);
                    XYZ origin = new XYZ(0, 0, 0);
                    Plane pl = new Plane(normal, origin);

                    SketchPlane sp = SketchPlane.Create(uiapp.ActiveUIDocument.Document, pl);
    
                    Revolution sphereR = uiapp.ActiveUIDocument.Document.FamilyCreate.NewRevolution(true, sphereProfile, sp, line, 0, 360);
                      
                    return sphereR.Id.IntegerValue;
                     */

                    FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                    failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                    t.SetFailureHandlingOptions(failOp);

                    XYZ begin = new XYZ(P0Coordx, P0Coordy, P0Coordz);
                    XYZ end = new XYZ(P1Coordx, P1Coordy, P1Coordz);
                    XYZ top = new XYZ(P2Coordx, P2Coordy, P2Coordz);

                    Arc arc = Arc.Create(begin, end, top);

                    Line line = Line.CreateBound(arc.GetEndPoint(1), arc.GetEndPoint(0));

                    XYZ normal = new XYZ(0, 0, 1);
                    XYZ origin = new XYZ((end.X + begin.X) / 2, (end.Y + begin.Y) / 2, (end.Z + begin.Z) / 2);
                    Plane pl = new Plane(normal, origin);

                    SketchPlane sp = SketchPlane.Create(uiapp.ActiveUIDocument.Document, pl);

                    ReferenceArray halfSphereBase = new ReferenceArray();

                    ModelArc sphereArc = uiapp.ActiveUIDocument.Document.FamilyCreate.NewModelCurve(arc, sp) as ModelArc;

                    ModelLine refLine = uiapp.ActiveUIDocument.Document.FamilyCreate.NewModelCurve(line, sp) as ModelLine;

                    halfSphereBase.Append(sphereArc.GeometryCurve.Reference);
                    halfSphereBase.Append(refLine.GeometryCurve.Reference);
                    FormArray sphereR = uiapp.ActiveUIDocument.Document.FamilyCreate.NewRevolveForms(true, halfSphereBase, refLine.GeometryCurve.Reference, 0, 360);

                    IList<int> ids = new List<int>();

                    foreach (Form f in sphereR)
                    {
                        ids.Add(f.Id.IntegerValue);
                    }

                    t.Commit();

                    return ids;
                }
        }

        public int DrawSphereMetric(double P0Coordx, double P0Coordy, double P0Coordz,
                               double P1Coordx, double P1Coordy, double P1Coordz,
                               double P2Coordx, double P2Coordy, double P2Coordz)
        {

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a sphere"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);
                
                XYZ begin = new XYZ(P0Coordx, P0Coordy, P0Coordz);
                XYZ end = new XYZ(P1Coordx, P1Coordy, P1Coordz);
                XYZ top = new XYZ(P2Coordx, P2Coordy, P2Coordz);

                Arc arc = Arc.Create(begin, end, top);

                Line line = Line.CreateBound(arc.GetEndPoint(1), arc.GetEndPoint(0));

                CurveArray halfSphereBase = new CurveArray();
                halfSphereBase.Append(arc);
                halfSphereBase.Append(line);

                CurveArrArray sphereProfile = new CurveArrArray();
                sphereProfile.Append(halfSphereBase);

                XYZ normal = new XYZ(0, 0, 1);
                XYZ origin = new XYZ(0, 0, 0);
                Plane pl = new Plane(normal, origin);

                SketchPlane sp = SketchPlane.Create(uiapp.ActiveUIDocument.Document, pl);
    
                Revolution sphereR = uiapp.ActiveUIDocument.Document.FamilyCreate.NewRevolution(true, sphereProfile, sp, line, 0, 360);

                t.Commit();

                return sphereR.Id.IntegerValue;
                
            }
        }

        public int DrawCylinder(double P0Coordx, double P0Coordy, double P0Coordz, double radius, double height)
        {
            
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a cylinder"))
            {
                t.Start();

                /*XYZ center = new XYZ(P0Coordx, P0Coordy, P0Coordz);

                CurveArrArray cylinder = new CurveArrArray();
                CurveArray cylinderArray = new CurveArray();

                XYZ arcCenter = center;
                Arc firstArc = Arc.Create(arcCenter, radius, 0, Math.PI, XYZ.BasisX, XYZ.BasisY);
                Arc secondArc = Arc.Create(arcCenter, radius, Math.PI, 2 * Math.PI, XYZ.BasisX, XYZ.BasisY);

                cylinderArray.Append(firstArc);
                cylinderArray.Append(secondArc);

                cylinder.Append(cylinderArray);

                XYZ normal = new XYZ(0, 0, 1);
                XYZ origin = new XYZ(0, 0, 0);
                Plane pl = new Plane(normal, origin);

                SketchPlane sp = SketchPlane.Create(uiapp.ActiveUIDocument.Document, pl);

                Extrusion cylinderEX = uiapp.ActiveUIDocument.Document.FamilyCreate.NewExtrusion(true, cylinder, sp, height);
                 
                return cylinderEX.Id.IntegerValue;*/

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                ReferenceArray cylinderArray = new ReferenceArray();

                XYZ center = new XYZ(P0Coordx, P0Coordy, P0Coordz);
                XYZ arcCenter = center;
                Arc firstArc = Arc.Create(arcCenter, radius, 0, Math.PI, XYZ.BasisX, XYZ.BasisY);
                Arc secondArc = Arc.Create(arcCenter, radius, Math.PI, 2 * Math.PI, XYZ.BasisX, XYZ.BasisY);

                XYZ normal = new XYZ(0, 0, 1);
                XYZ origin = new XYZ(0, 0, 0);
                Plane pl = new Plane(normal, origin);

                SketchPlane sp = SketchPlane.Create(uiapp.ActiveUIDocument.Document, pl);

                ModelArc arc1 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewModelCurve(firstArc, sp) as ModelArc;
                ModelArc arc2 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewModelCurve(secondArc, sp) as ModelArc;


                cylinderArray.Append(arc1.GeometryCurve.Reference);
                cylinderArray.Append(arc2.GeometryCurve.Reference);

                XYZ direction = new XYZ (0, 0, height);

                Form cylinderEX = uiapp.ActiveUIDocument.Document.FamilyCreate.NewExtrusionForm(true, cylinderArray, direction);

                t.Commit();

                return cylinderEX.Id.IntegerValue;

            }
        }

        public int DrawCylinder(double P0Coordx, double P0Coordy, double P0Coordz, double radius, double P1Coordx, double P1Coordy, double P1Coordz)
        {

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a cylinder"))
            {
                t.Start();
                /*
                XYZ center = new XYZ(P0Coordx, P0Coordy, P0Coordz);

                XYZ top = new XYZ(P1Coordx, P1Coordy, P1Coordz);

                CurveArrArray arrarr = new CurveArrArray();
                CurveArray curveArr1 = new CurveArray();

                Plane plane = new Plane(XYZ.BasisZ, XYZ.Zero);  //XY plane
                Arc arc1 = Arc.Create(plane, radius, 0, Math.PI);
                Arc arc2 = Arc.Create(plane, radius, Math.PI, Math.PI * 2);
                curveArr1.Append(arc1);
                curveArr1.Append(arc2);
                arrarr.Append(curveArr1);

                SweepProfile profile = uiapp.Application.Create.NewCurveLoopsProfile(arrarr);

                XYZ v = new XYZ(top.X - center.X, top.Y - center.Y, top.Z - center.Z);

                double height = v.GetLength();
                XYZ centerBase = new XYZ(0, 0, 0);
                XYZ centerTop = new XYZ(0, 0, height);

                Curve curve = Line.CreateBound(centerBase, centerTop);

                CurveArray path = new CurveArray();
                path.Append(curve);

                SketchPlane pathPlane = SketchPlane.Create(uiapp.ActiveUIDocument.Document, new Plane(XYZ.BasisY, Autodesk.Revit.DB.XYZ.Zero));

                Sweep sweep1 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewSweep(true, path, pathPlane, profile, 0, ProfilePlaneLocation.Start);
                 
                 double phi = (v.X == 0 && v.Y == 0) ? 0 : Math.Atan2(v.Y, v.X);
                double psi = (v.X == 0 && v.Y == 0 && v.Z == 0) ? 0 : Math.Atan2(Math.Sqrt(v.X * v.X + v.Y * v.Y), v.Z);

                if (psi != 0)
                {
                    
                        Line axis = Line.CreateBound(XYZ.Zero, new XYZ(0, 1, 0));
                        ElementTransformUtils.RotateElement(uiapp.ActiveUIDocument.Document, sweep1.Id, axis, psi);
                    
                }

                if (phi != 0)
                {
                    
                        Line axis = Line.CreateBound(XYZ.Zero, new XYZ(0, 0, 1));
                        ElementTransformUtils.RotateElement(uiapp.ActiveUIDocument.Document, sweep1.Id, axis, phi);
                    
                }

                ElementTransformUtils.MoveElement(uiapp.ActiveUIDocument.Document, sweep1.Id, center);
                 */

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                ReferenceArray cylinderArray = new ReferenceArray();

                XYZ center = new XYZ(P0Coordx, P0Coordy, P0Coordz);

                XYZ top = new XYZ(P1Coordx, P1Coordy, P1Coordz);

                XYZ direction = new XYZ(top.X - center.X, top.Y - center.Y, top.Z - center.Z);


                Plane plane = new Plane(direction, center);
                SketchPlane sp = SketchPlane.Create(uiapp.ActiveUIDocument.Document, plane);

                Arc arc1 = Arc.Create(plane, radius, 0, Math.PI);
                Arc arc2 = Arc.Create(plane, radius, Math.PI, Math.PI * 2);

                ModelArc arc11 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewModelCurve(arc1, sp) as ModelArc;
                ModelArc arc22 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewModelCurve(arc2, sp) as ModelArc;

                cylinderArray.Append(arc11.GeometryCurve.Reference);
                cylinderArray.Append(arc22.GeometryCurve.Reference);

                Form sweep1 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewExtrusionForm(true, cylinderArray, direction);

                t.Commit();

                return sweep1.Id.IntegerValue;
            }
        }

        public int DrawCylinderMetric(double P0Coordx, double P0Coordy, double P0Coordz, double radius, double P1Coordx, double P1Coordy, double P1Coordz)
        {

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a cylinder in a metric document"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);
                
                XYZ center = new XYZ(P0Coordx, P0Coordy, P0Coordz);

                XYZ top = new XYZ(P1Coordx, P1Coordy, P1Coordz);

                CurveArrArray arrarr = new CurveArrArray();
                CurveArray curveArr1 = new CurveArray();

                Plane plane = new Plane(XYZ.BasisZ, XYZ.Zero);  //XY plane
                Arc arc1 = Arc.Create(plane, radius, 0, Math.PI);
                Arc arc2 = Arc.Create(plane, radius, Math.PI, Math.PI * 2);
                curveArr1.Append(arc1);
                curveArr1.Append(arc2);
                arrarr.Append(curveArr1);

                SweepProfile profile = uiapp.Application.Create.NewCurveLoopsProfile(arrarr);

                XYZ v = new XYZ(top.X - center.X, top.Y - center.Y, top.Z - center.Z);

                double height = v.GetLength();
                XYZ centerBase = new XYZ(0, 0, 0);
                XYZ centerTop = new XYZ(0, 0, height);

                Curve curve = Line.CreateBound(centerBase, centerTop);

                CurveArray path = new CurveArray();
                path.Append(curve);

                SketchPlane pathPlane = SketchPlane.Create(uiapp.ActiveUIDocument.Document, new Plane(XYZ.BasisY, Autodesk.Revit.DB.XYZ.Zero));

                Sweep sweep1 = uiapp.ActiveUIDocument.Document.FamilyCreate.NewSweep(true, path, pathPlane, profile, 0, ProfilePlaneLocation.Start);
                 
                 double phi = (v.X == 0 && v.Y == 0) ? 0 : Math.Atan2(v.Y, v.X);
                double psi = (v.X == 0 && v.Y == 0 && v.Z == 0) ? 0 : Math.Atan2(Math.Sqrt(v.X * v.X + v.Y * v.Y), v.Z);

                if (psi != 0)
                {
                    
                        Line axis = Line.CreateBound(XYZ.Zero, new XYZ(0, 1, 0));
                        ElementTransformUtils.RotateElement(uiapp.ActiveUIDocument.Document, sweep1.Id, axis, psi);
                    
                }

                if (phi != 0)
                {
                    
                        Line axis = Line.CreateBound(XYZ.Zero, new XYZ(0, 0, 1));
                        ElementTransformUtils.RotateElement(uiapp.ActiveUIDocument.Document, sweep1.Id, axis, phi);
                    
                }

                ElementTransformUtils.MoveElement(uiapp.ActiveUIDocument.Document, sweep1.Id, center);
                t.Commit();

                return sweep1.Id.IntegerValue;
            }
        }
        public int CreateMassSweep(IList<double> profileList1, IList<double> pathList, IList<double> profileList2)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Create a sweep"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                int id = 0;

                List<XYZ> profile1Points = new List<XYZ>();
                List<XYZ> pathPoints = new List<XYZ>();
                List<XYZ> profile2Points = new List<XYZ>();

                for (int i = 0; i < profileList1.Count - 2; i = i + 3)
                {

                    XYZ point = new XYZ(profileList1[i], profileList1[i + 1], profileList1[i + 2]);

                    profile1Points.Add(point);
                }

                for (int i = 0; i < profileList2.Count - 2; i = i + 3)
                {

                    XYZ point = new XYZ(profileList2[i], profileList2[i + 1], profileList2[i + 2]);

                    profile2Points.Add(point);
                }

                for (int j = 0; j < pathList.Count - 2; j = j + 3)
                {

                    XYZ point = new XYZ(pathList[j], pathList[j + 1], pathList[j + 2]);

                    pathPoints.Add(point);
                }

                ReferenceArray refar = new ReferenceArray();

                ReferenceArrayArray refarar = new ReferenceArrayArray();

                ReferencePointArray rpa = new ReferencePointArray();

                ReferencePoint rp = null;

                CurveByPoints cbp = null;

                foreach (XYZ p in profile1Points)
                {
                    rp = doc.FamilyCreate.NewReferencePoint(p);
                    rpa.Append(rp);
                }

                cbp = doc.FamilyCreate.NewCurveByPoints(rpa);
                refar.Append(cbp.GeometryCurve.Reference);
                refarar.Append(refar);
                rpa.Clear();
                refar = new ReferenceArray();

                foreach (XYZ p in profile2Points)
                {
                    rp = doc.FamilyCreate.NewReferencePoint(p);
                    rpa.Append(rp);
                }

                cbp = doc.FamilyCreate.NewCurveByPoints(rpa);
                refar.Append(cbp.GeometryCurve.Reference);
                refarar.Append(refar);
                rpa.Clear();
                refar = new ReferenceArray();

                foreach (XYZ p in pathPoints)
                {
                    rp = doc.FamilyCreate.NewReferencePoint(p);
                    rpa.Append(rp);
                }

                cbp = doc.FamilyCreate.NewCurveByPoints(rpa);

                refar.Append(cbp.GeometryCurve.Reference);

                try
                {

                    Form sweep = doc.FamilyCreate.NewSweptBlendForm(true, refar, refarar);

                    id = sweep.Id.IntegerValue;
                }

                catch (Exception e)
                {
                    TaskDialog.Show("Revit", e.ToString());
                }

                t.Commit();
                return id;

            }
        }

        public int CreateExtrusionMass(IList<double> ptsList, double elevation)
        {

            int id = 0;

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Create a sweep"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                try
                {
                    Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                    List<XYZ> profilePoints = new List<XYZ>();

                    for (int i = 0; i < ptsList.Count - 2; i = i + 3)
                    {

                        XYZ point = new XYZ(ptsList[i], ptsList[i + 1], ptsList[i + 2]);

                        profilePoints.Add(point);
                    }

                    XYZ normal = new XYZ(0, 0, 1);
                    XYZ origin = new XYZ(0, 0, 0);
                    Plane pl = new Plane(normal, origin);
                    SketchPlane sp = SketchPlane.Create(uiapp.ActiveUIDocument.Document, pl);
                  
                    ReferenceArray refAr = new ReferenceArray();

                    for (int j = 0; j < profilePoints.Count - 1; j++)
                    {
                        Line l = Line.CreateBound(profilePoints[j], profilePoints[j + 1]);
                        ModelLine ml = doc.FamilyCreate.NewModelCurve(l, sp) as ModelLine;
                        refAr.Append(ml.GeometryCurve.Reference);
                    }

                    XYZ direction = new XYZ(0, 0, elevation);


                    Form extrusionMass = doc.FamilyCreate.NewExtrusionForm(true, refAr, direction);
                    id = extrusionMass.Id.IntegerValue;
                }
                catch (Exception e)
                {
                    TaskDialog.Show("Revit", e.ToString());
                }

                t.Commit();
            }

            return id;
        }

        public void MoveElement(int elementInt, double transX, double transY, double transZ)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Move an element"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                ElementId elementID = new ElementId(elementInt);

                XYZ translation = new XYZ(transX, transY, transZ);

                ElementTransformUtils.MoveElement(doc, elementID, translation);

                t.Commit();
            }
        }

        public void RotateElement(int elementInt, double angle, double axis0X, double axis0Y, double axis0Z, double axis1X, double axis1Y, double axis1Z)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Move an element"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                ElementId elementID = new ElementId(elementInt);

                Line axis = Line.CreateBound(new XYZ(axis0X, axis0Y, axis0Z), new XYZ(axis1X, axis1Y, axis1Z));

                ElementTransformUtils.RotateElement(doc, elementID, axis, angle);

                t.Commit();
            }
        }
        public void SurfaceGrid(IList<IList<XYZ>> matrix)
        {
           

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Create a surface grid"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;
              
                Form loftForm = null;

                ReferencePointArray rpa = new ReferencePointArray();
                ReferenceArrayArray refarar = new ReferenceArrayArray();
                ReferenceArray refar = new ReferenceArray();
                ReferencePoint rp = null;
                XYZ point = null;

                TaskDialog.Show("Revit", matrix.Count.ToString());

                foreach (IList<XYZ> l in matrix)
                {
                    foreach (XYZ p in l)
                    {
                        point = doc.Application.Create.NewXYZ(p.X, p.Y, p.Z);

                        try
                        {
                            rp = doc.FamilyCreate.NewReferencePoint(p);
                        }
                        catch (Exception e)
                        {
                            TaskDialog.Show("Revit", e.ToString());
                        }
                        rpa.Append(rp);
                    }

                    CurveByPoints cbp = null;

                    cbp = doc.FamilyCreate.NewCurveByPoints(rpa);
                    refar.Append(cbp.GeometryCurve.Reference);
                    refarar.Append(refar);
                    rpa.Clear();
                    refar = new ReferenceArray();
                }

                TaskDialog.Show("Revit", "Vi a matrix");
                
               try
               {
                   loftForm = doc.FamilyCreate.NewLoftForm(true, refarar);
               }
               catch (Exception e)
               {
                   TaskDialog.Show("Revit", e.ToString());
               }
               
               t.Commit();
            }
        }

        public void Union(IList<int> ids)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Unites elements"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                ElementId id1;

                ElementId id2;

                for (int i = 0; i < ids.Count - 1; i++)
                {
                   id1 = new ElementId(ids[i]);
                   id2 = new ElementId(ids[i + 1]);

                   JoinGeometryUtils.JoinGeometry(doc, doc.GetElement(id1), doc.GetElement(id2));
                }

                t.Commit();
            }
        }

        public void showDisconnectMessage()
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Show disconnect message to user."))
            {
                t.Start();

                TaskDialog.Show("Revit", "Disconnected from Rosetta");

                t.Commit();
            }
        }

        public void CreateTables()
        {


            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a tables"))
            {

                t.Start();
                Autodesk.Revit.DB.Document document = uiapp.ActiveUIDocument.Document;

                String fileName = @"C:\ProgramData\Autodesk\RVT 2015\Libraries\Portugal\Furniture\Tables\M_Table-Dining Round w Chairs.rfa";

                // try to load family
                Family family = null;
                if (!document.LoadFamily(fileName, out family))
                {
                    throw new Exception("Unable to load " + fileName);
                }

                // Loop through table symbols and add a new table for each
                ISet<ElementId> familySymbolIds = family.GetFamilySymbolIds();
                double x = 0.0, y = 0.0;
                foreach (ElementId id in familySymbolIds)
                {
                    FamilySymbol symbol = family.Document.GetElement(id) as FamilySymbol;
                    XYZ location = new XYZ(x, y, 10.0);

                    FamilyInstance instance = document.Create.NewFamilyInstance(location, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    x += 10.0;
                }

                t.Commit();
            }
        }

        public Arc GetArc(XYZ center, double radius)
        {
            Arc arc = Arc.Create(center - radius * XYZ.BasisZ, center + radius * XYZ.BasisZ, center + radius * XYZ.BasisX);
            return arc;
            
        }

        public Curve GetLinearCurve (XYZ pointA, XYZ pointB)
        {
            Curve curve = Line.CreateBound(pointA, pointB);

            XYZ normal = new XYZ(0, 1, 0);
            XYZ origin = new XYZ(0, 0, 0);
            Plane pl = new Plane(normal, origin);

            SketchPlane sketch = SketchPlane.Create(uiapp.ActiveUIDocument.Document, pl);

            ModelCurve m = uiapp.ActiveUIDocument.Document.Create.NewModelCurve(curve, sketch);

            return curve;
        }

        public XYZ GetPoint(double x, double y, double z)
        {
            XYZ point = new XYZ(x, y, z);
            return point;
        }

        public int CreateWall(double P0Coordx, double P0Coordy, double P0Coordz, double P1Coordx, double P1Coordy, double P1Coordz, int levelId)
        {

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a new wall"))
            {

                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                int id = 0;

                XYZ p0 = new XYZ(P0Coordx, P0Coordy, P0Coordz);
                XYZ p1 = new XYZ(P1Coordx, P1Coordy, P1Coordz);

                Line line = Line.CreateBound(p0, p1);
                Level level = null;

                ElementId levelIdEl= new ElementId(levelId);

                level = doc.GetElement(levelIdEl) as Level;

                if(level == null){
                    TaskDialog.Show("Revit", "Invalid leve");
                }
                else{

                    Wall wall = Wall.Create(doc, line, level.Id, false);

                    /*try
                    {
                        WallType wt = wall.WallType;
                        WallType newWallType = wt.Duplicate("MyWall") as WallType;

                        FilteredElementCollector elementCollector = new FilteredElementCollector(doc);
                        elementCollector.WherePasses(new ElementClassFilter(typeof(Material)));
                        IList<Element> materials = elementCollector.ToElements();

                        CompoundStructure cs = newWallType.GetCompoundStructure();
                        IList<CompoundStructureLayer> layers = newWallType.GetCompoundStructure().GetLayers();

                        Material wm = null;

                        int layerIndex = 0;

                        foreach (Material m in materials)
                        {
                            if (m.Name.ToString().Equals("Glass"))
                            {
                                wm = m;
                                break;
                            }
                        }

                        foreach (CompoundStructureLayer l in layers)
                        {
                            cs.SetMaterialId(layerIndex, wm.Id);
                            layerIndex++;
                        }

                        newWallType.SetCompoundStructure(cs);

                        wall.WallType = newWallType;
                    }
                    catch (Exception e)
                    {
                        TaskDialog.Show("Revit", e.ToString());
                    }*/

                    doc.Regenerate();
                    doc.AutoJoinElements();

                    id = wall.Id.IntegerValue;
                }

                t.Commit();

                return id;
            }   
           
        }

        public int CreateWall(double P0Coordx, double P0Coordy, double P0Coordz, double P1Coordx, double P1Coordy, double P1Coordz, double height, int levelId)
        {

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a new wall"))
            {

                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                int id = 0;

                XYZ p0 = new XYZ(P0Coordx, P0Coordy, P0Coordz);
                XYZ p1 = new XYZ(P1Coordx, P1Coordy, P1Coordz);

                Line line = Line.CreateBound(p0, p1);
                Level level = null;

                ElementId levelIdEl = new ElementId(levelId);

                level = doc.GetElement(levelIdEl) as Level;

                if (level == null)
                {
                    TaskDialog.Show("Revit", "Invalid leve");
                }
                else
                {

                    Element wall = Wall.Create(doc, line, level.Id, false);

                    wall.get_Parameter("Unconnected Height").Set(height);

                    doc.Regenerate();
                    doc.AutoJoinElements();

                    id = wall.Id.IntegerValue;
                }

                t.Commit();

                return id;
            }

        }

        public int CreateWall(double P0Coordx, double P0Coordy, double P0Coordz, double P1Coordx, double P1Coordy, double P1Coordz, int baseLevelId, int topLevelId)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a new wall with levels"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                int id = 0;

                XYZ p0 = new XYZ(P0Coordx, P0Coordy, P0Coordz);
                XYZ p1 = new XYZ(P1Coordx, P1Coordy, P1Coordz);

                Line line = Line.CreateBound(p0, p1);

                Level level0 = null;
                Level level1 = null;

                ElementId baseLevelIdEl = new ElementId(baseLevelId);

                level0 = doc.GetElement(baseLevelIdEl) as Level;

                ElementId topLevelIdEl = new ElementId(topLevelId);

                level1 = doc.GetElement(topLevelIdEl) as Level;

                if (level0 == null || level1 == null)
                {
                    TaskDialog.Show("Revit", "Invalid levels");
                }
                else
                {

                    Element wall = Wall.Create(doc, line, level0.Id, false);

                    try
                    {
                        wall.get_Parameter("Top Constraint").Set(level1.Id);

                        TaskDialog.Show("Revit", "Cheguei aqui");
                        doc.Regenerate();
                        doc.AutoJoinElements();

                        id = wall.Id.IntegerValue;
                    }

                    catch (Exception e)
                    {
                        TaskDialog.Show("Revit", e.ToString());
                    }

                    
                }
                t.Commit();

                return id;
            }
        }

        public IList<int> CreatePolyWalls(IList<double> points, int baseLevelId, int topLevelId, int famID)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating various walls"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                IList<int> ids = new List<int>();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                IList<XYZ> pts = new List<XYZ>();

                for (int i = 0; i < points.Count; i = i + 3)
                {
                    XYZ p = new XYZ(points[i], points[i + 1], points[i + 2]);

                    pts.Add(p);
                }

                Level level0 = null;
                Level level1 = null;

                ElementId baseLevelIdEl = new ElementId(baseLevelId);

                level0 = doc.GetElement(baseLevelIdEl) as Level;

                ElementId topLevelIdEl = new ElementId(topLevelId);

                level1 = doc.GetElement(topLevelIdEl) as Level;

                if (level0 == null || level1 == null)
                {
                    TaskDialog.Show("Revit", "Invalid leve");
                }
                else
                {
                    try
                    {


                        for (int i = 0; i < pts.Count - 1; i++)
                        {
                            Line line = Line.CreateBound(pts[i], pts[i + 1]);

                            Wall wall = Wall.Create(doc, line, level0.Id, false) as Wall;

                            if (famID != 0)
                            {
                                wall.WallType = doc.GetElement(new ElementId(famID)) as WallType;
                            }

                            wall.get_Parameter("Top Constraint").Set(level1.Id);

                            ids.Add(wall.Id.IntegerValue);
                        }

                        
                    }
                    catch (Exception e)
                    {
                        TaskDialog.Show("Revit", e.ToString());
                    }
                }

                t.Commit();
                return ids;
            }
        }

        public List<RosettaApp.MyWall> GetWallsInfo()
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            List<Wall> walls = new FilteredElementCollector(doc).WherePasses(new ElementClassFilter(typeof(Wall), false)).Cast<Wall>().ToList();

            List<RosettaApp.MyWall> resultInfo = new List<RosettaApp.MyWall>();

            foreach (Wall w in walls)
            {
                ElementId tpID = w.get_Parameter("Top Constraint").AsElementId();

                Level tl = doc.GetElement(tpID) as Level;

                Level bl = doc.GetElement(w.LevelId) as Level;

                LocationCurve aux = w.Location as LocationCurve;

                XYZ ep0 = aux.Curve.GetEndPoint(0);

                XYZ ep1 = aux.Curve.GetEndPoint(1);

                string type = w.WallType.Name.ToString();

                RosettaApp.MyLevel bottomLevel = new RosettaApp.MyLevel(bl.Name, bl.Elevation);

                RosettaApp.MyLevel topLevel = new RosettaApp.MyLevel(tl.Name, tl.Elevation);

                RosettaApp.MyWall myWall = new RosettaApp.MyWall(ep0, ep1, bottomLevel, topLevel, type);

                resultInfo.Add(myWall);
            }

            return resultInfo;
        }

        public IList<int> CreateWallsFromSlabs(int slabID, int baseLevelId, double height)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating various walls from slab geometry"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                IList<int> ids = new List<int>();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                Floor floor = (Floor)doc.GetElement(new ElementId(slabID));

                ElementId bottomID = new ElementId(baseLevelId);

                List<List<XYZ>> polygon = new List<List<XYZ>>();

                GeometryElement floorGeo = floor.get_Geometry(uiapp.Application.Create.NewGeometryOptions());

                try
                {
                    foreach (GeometryObject obj in floorGeo)
                    {
                        Solid solid = obj as Solid;

                        if (solid != null)
                        {
                            GetBoundary(polygon, solid);
                        }
                    }

                    FilteredElementCollector elementCollector = new FilteredElementCollector(doc);
                    elementCollector.WherePasses(new ElementClassFilter(typeof(Material)));
                    IList<Element> materials = elementCollector.ToElements();

                    Material wm = null;

                    foreach (Material m in materials)
                    {
                        if (m.Name.ToString().Equals("Glass"))
                        {
                            wm = m;
                            break;
                        }
                    }

                    WallType wt = null;
                    WallType newWallType = null;

                    CompoundStructure cs = null;

                    IList<CompoundStructureLayer> layers = null;

                    int layerIndex = 0;

                    foreach (List<XYZ> pts in polygon)
                    {
                        Line lf = Line.CreateBound(pts[0], pts[pts.Count - 1]);

                        Wall wallf = Wall.Create(doc, lf, bottomID, false);
                        
                        IList<WallType> desiredType = new FilteredElementCollector(doc).OfClass(typeof(WallType)).Cast<WallType>().Where<WallType>(wallType => wallType.Name.Equals("MyWall")).ToList<WallType>();

                        if (desiredType.Count == 0)
                        {
                            wt = wallf.WallType;
                            newWallType = wt.Duplicate("MyWall") as WallType;

                            cs = newWallType.GetCompoundStructure();
                            layers = newWallType.GetCompoundStructure().GetLayers();

                            layerIndex = 0;

                            foreach (CompoundStructureLayer l in layers)
                            {
                                cs.SetMaterialId(layerIndex, wm.Id);
                                layerIndex++;
                            }

                            newWallType.SetCompoundStructure(cs);
                        }
                        else{
                            newWallType = desiredType.First();
                        }

                        wallf.get_Parameter("Unconnected Height").Set(height);
                        wallf.WallType = newWallType;

                        ids.Add(wallf.Id.IntegerValue);

                        for (int i = 0; i < pts.Count - 1; i++)
                        {
                            Line line = Line.CreateBound(pts[i], pts[i + 1]);

                            Wall wall = Wall.Create(doc, line, bottomID, false);

                            wall.get_Parameter("Unconnected Height").Set(height);

                            wall.WallType = newWallType;

                            ids.Add(wall.Id.IntegerValue);
                        }

                        
                    }
                }
                catch (Exception e)
                {
                    TaskDialog.Show("Revit", e.ToString());
                }

                t.Commit();
                return ids;
            }
        }

        public int CreateCurtainWall(double P0Coordx, double P0Coordy, double P0Coordz, 
                                     double P1Coordx, double P1Coordy, double P1Coordz,
                                     IList<double> uLineCoords, IList<double> vLineCoords,
                                     int baseLevelID, int topLevelID)
        {
            int id = 0;
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a new curtain wall"))
            {
                t.Start();


                // Build a wall profile for the wall creation
                XYZ first = new XYZ(P0Coordx, P0Coordy, P0Coordz);
                XYZ second = new XYZ(P1Coordx, P1Coordy, P1Coordz);

                FilteredElementCollector filteredElementCollector = new FilteredElementCollector(doc);
                filteredElementCollector.OfClass(typeof(WallType));
                List<WallType> curtainWallTypes = filteredElementCollector.Cast<WallType>().Where<WallType>(wallType => wallType.Kind == WallKind.Curtain).ToList<WallType>();

                Line line = Line.CreateBound(first, second);

                Level baseLevel = null;

                Level topLevel = null;

                ElementId baseLevelEID = new ElementId(baseLevelID);

                ElementId topLevelEID = new ElementId(topLevelID);

                baseLevel = doc.GetElement(baseLevelEID) as Level;

                topLevel = doc.GetElement(topLevelEID) as Level;

                
                

                if (topLevel == null || baseLevel == null)
                {
                    TaskDialog.Show("Revit", "Invalid leve");
                }
                else
                {
                    try
                    {
                        Wall cw = Wall.Create(doc, line, baseLevel.Id, false);

                        cw.WallType = curtainWallTypes.First();

                        cw.get_Parameter("Top Constraint").Set(topLevel.Id);

                        id = cw.Id.IntegerValue;
                        
                       
                    }

                    catch (Exception e)
                    {
                        TaskDialog.Show("Revit", e.ToString());
                    }
                }
                

                t.Commit();
            }
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating grid"))
            {
                t.Start();

                Wall cw = doc.GetElement(new ElementId(id)) as Wall;

                for (int i = 0; i < uLineCoords.Count; i += 3 )
                {
                   cw.CurtainGrid.AddGridLine(true, new XYZ(uLineCoords[i], uLineCoords[i+1], uLineCoords[i+2]), true);
                }

                for (int i = 0; i < vLineCoords.Count; i += 3)
                {
                    cw.CurtainGrid.AddGridLine(false, new XYZ(vLineCoords[i], vLineCoords[i+1], vLineCoords[i+2]), false);
                }

                List<MullionType> mullionTypes = new FilteredElementCollector(doc).OfClass(typeof(MullionType)).Cast<MullionType>().ToList<MullionType>();


                foreach (ElementId glID in cw.CurtainGrid.GetVGridLineIds().ToList())
                {
                    CurtainGridLine gl = doc.GetElement(glID) as CurtainGridLine;

                    foreach (Curve c in gl.AllSegmentCurves)
                    {
                        gl.AddMullions(c, mullionTypes.First(), true);
                    }
                }

                foreach (ElementId glID in cw.CurtainGrid.GetUGridLineIds().ToList())
                { 
                    CurtainGridLine gl = doc.GetElement(glID) as CurtainGridLine;

                    foreach (Curve c in gl.AllSegmentCurves)
                    {
                        gl.AddMullions(c, mullionTypes.First(), true);
                    }
                }

                t.Commit();
            }

            return id;

        }


        public int CreateSlabWall(double bottomleftx, double bottomlefty, double bottomleftz,
                                  double topleftx, double toplefty, double topleftz,
                                  double bottomrightx, double bottomrighty, double bottomrightz,
                                  double toprightx, double toprighty, double toprightz,
                                  int levelID)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a new slabWall"))
            {
                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                int id = 0;

                t.Start();

                XYZ p0 = new XYZ(bottomleftx, bottomlefty, bottomleftz);
                XYZ p1 = new XYZ(topleftx, toplefty, topleftz);
                XYZ p2 = new XYZ(toprightx, toprighty, toprightz);
                XYZ p3 = new XYZ(bottomrightx, bottomrighty, bottomrightz);

                XYZ aux1 = new XYZ(p1.X, p1.Y, p0.Z);

                Line l1 = Line.CreateBound(p0, p1);

                double slope = 0;

                if (p0.Y == p1.Y)
                {
                    slope = 90;
                }
                else if (p0.Z == p1.Z)
                {
                    slope = 0;
                }

                else
                {
                    Line l2 = Line.CreateBound(aux1, p0);
                    
                    slope = Math.Acos(l2.Length / l1.Length);
                }

                XYZ pp1 = new XYZ(p1.X, (p1.Z - p0.Z), p0.Z);
                XYZ pp2 = new XYZ(p2.X, (p2.Z - p3.Z), p3.Z);

                Level level = null;

                ElementId levelIdEl = new ElementId(levelID);

                level = doc.GetElement(levelIdEl) as Level;

                if (level == null)
                {
                    TaskDialog.Show("Revit", "Invalid level");
                }
                else
                {
                    try
                    {
                        FloorType floorType = new FilteredElementCollector(doc).OfClass(typeof(FloorType)).First() as FloorType;

                        CurveArray profile = new CurveArray();

                        profile.Append(Line.CreateBound(p0, pp1));
                        profile.Append(Line.CreateBound(pp1, pp2));
                        profile.Append(Line.CreateBound(pp2, p3));
                        profile.Append(Line.CreateBound(p3, p0));

                        TaskDialog.Show("Revit", slope.ToString());

                        Floor floor = doc.Create.NewSlab(profile, level, Line.CreateBound(p0, pp1), slope, true);

                        floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM).Set(0); //Below the level line

                        id = floor.Id.IntegerValue;
                    }
                    catch (Exception e)
                    {
                        TaskDialog.Show("revit", e.ToString());
                    }

                }

                t.Commit();

                return id;
            }
        }

        public int CreateMassWall(double bottomleftx, double bottomlefty, double bottomleftz,
                                  double topleftx, double toplefty, double topleftz,
                                  double bottomrightx, double bottomrighty, double bottomrightz,
                                  double toprightx, double toprighty, double toprightz,
                                  double height, int levelID)
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;


            bool glassExists = true;
            int id = 0;

            XYZ p0 = new XYZ(bottomleftx, bottomlefty, bottomleftz);
            XYZ p1 = new XYZ(topleftx, toplefty, topleftz);
            XYZ p2 = new XYZ(toprightx, toprighty, toprightz);
            XYZ p3 = new XYZ(bottomrightx, bottomrighty, bottomrightz);

            const string conceptualMassTemplatePath = "C:/ProgramData/Autodesk/RVT 2015/Family Templates/English/Conceptual Mass/Metric Mass.rft";

            const string familyPath = "C:/Autodesk/TestFamily.rfa";

            Autodesk.Revit.DB.Document massDoc = uiapp.Application.NewFamilyDocument(conceptualMassTemplatePath);

            using (Transaction t1 = new Transaction(massDoc, "Create mass"))
            {
                t1.Start();

                FailureHandlingOptions failOp = t1.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t1.SetFailureHandlingOptions(failOp);

                ReferenceArray refar = new ReferenceArray();

                ReferenceArrayArray refarar = new ReferenceArrayArray();

                ReferencePointArray rpa = new ReferencePointArray();

                ReferencePoint rp = null;

                CurveByPoints cbp = null;

                XYZ aux = new XYZ(p1.X - p0.X, p1.Y - p0.Y, 0);

                aux = aux.Normalize();

                //First Curve

                rp = massDoc.FamilyCreate.NewReferencePoint(p0);
                rpa.Append(rp);

                rp = massDoc.FamilyCreate.NewReferencePoint(p3);
                rpa.Append(rp);

                cbp = massDoc.FamilyCreate.NewCurveByPoints(rpa);
                refar.Append(cbp.GeometryCurve.Reference);
                refarar.Append(refar);
                rpa.Clear();
                refar = new ReferenceArray();

                //Second Curve

                rp = massDoc.FamilyCreate.NewReferencePoint(p1);
                rpa.Append(rp);

                rp = massDoc.FamilyCreate.NewReferencePoint(p2);
                rpa.Append(rp);

                cbp = massDoc.FamilyCreate.NewCurveByPoints(rpa);
                refar.Append(cbp.GeometryCurve.Reference);
                refarar.Append(refar);

                Form loft = massDoc.FamilyCreate.NewLoftForm(true, refarar);

                t1.Commit();
            }

            SaveAsOptions opt = new SaveAsOptions();
            opt.OverwriteExistingFile = true;
           
            massDoc.SaveAs(familyPath, opt);
           
            massDoc.Close(true);

            using (Transaction t2 = new Transaction(doc, "Create FaceWall"))
            {
                t2.Start();

                FailureHandlingOptions failOp = t2.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t2.SetFailureHandlingOptions(failOp);

                if (!doc.LoadFamily(familyPath))
                    throw new Exception("Could not load family");

                Family family = new FilteredElementCollector(doc)
                                .OfClass(typeof(Family))
                                .Where<Element>(x => x.Name.Equals("TestFamily"))
                                .Cast<Family>()
                                .FirstOrDefault();

                FamilySymbol fs = doc.GetElement(family.GetFamilySymbolIds().First<ElementId>()) as FamilySymbol;

                Level level = doc.GetElement(new ElementId(levelID)) as Level;

                XYZ localPt = new XYZ(0, 0, 0);

                FamilyInstance fi = doc.Create.NewFamilyInstance(localPt, fs, level, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);


                doc.Regenerate();

                List<WallType> wallTypes = new FilteredElementCollector(doc)
                                        .OfClass(typeof(WallType))
                                        .Cast<WallType>()
                                        .Where<WallType>(x => x.Name.Equals("GlassWall"))
                                        .ToList<WallType>();
                WallType wallType = null;

                if (wallTypes.Count == 0)
                {
                    wallType = new FilteredElementCollector(doc)
                                        .OfClass(typeof(WallType))
                                        .Cast<WallType>()
                                        .Where<WallType>(x => FaceWall.IsWallTypeValidForFaceWall(doc, x.Id))
                                        .FirstOrDefault();

                    glassExists = false;
                }
                else
                {
                    wallType = wallTypes.First();
                }

                Options options = uiapp.Application.Create.NewGeometryOptions();
                options.ComputeReferences = true;
                
                GeometryElement geo = fi.get_Geometry(options);

                foreach (GeometryObject obj in geo)
                {
                    Solid solid = obj as Solid;

                    if (solid != null)
                    {
                        foreach (Face f in solid.Faces)
                        {
                            Debug.Assert(f.Reference != null, "no references");

                            PlanarFace pf = f as PlanarFace;

                            FaceWall faceWall = null;

                            if (pf != null)
                            {
                                XYZ n = pf.Normal;

                                if (n.X == 0 && n.Y == 0)
                                {

                                }
                                else
                                {
                                    faceWall= FaceWall.Create(doc, wallType.Id, WallLocationLine.CoreExterior, f.Reference);

                                }
                            }
                            else
                            {
                                faceWall = FaceWall.Create(doc, wallType.Id, WallLocationLine.CoreExterior, f.Reference);
   
                            }

                            if (!glassExists)
                            {
                                FilteredElementCollector elementCollector = new FilteredElementCollector(doc);
                                elementCollector.WherePasses(new ElementClassFilter(typeof(Material)));
                                IList<Element> materials = elementCollector.ToElements();

                                Material wm = null;

                                foreach (Material m in materials)
                                {
                                    if (m.Name.ToString().Equals("Glass"))
                                    {
                                        wm = m;
                                        break;
                                    }
                                }

                                CompoundStructure cs = null;

                                IList<CompoundStructureLayer> layers = null;

                                int layerIndex = 0;

                                WallType newWallType = wallType.Duplicate("GlassWall") as WallType;

                                cs = newWallType.GetCompoundStructure();
                                layers = newWallType.GetCompoundStructure().GetLayers();

                                layerIndex = 0;

                                foreach (CompoundStructureLayer l in layers)
                                {
                                    cs.SetMaterialId(layerIndex, wm.Id);
                                    layerIndex++;
                                }

                                newWallType.SetCompoundStructure(cs);

                                faceWall.ChangeTypeId(newWallType.Id);
                            }

                            id = faceWall.Id.IntegerValue;
                        }
                    }
                }

                doc.Delete(family.Id);

                t2.Commit();
            }
            return id;
        }

        public double GetWallVolume(int wallIntId)
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            Wall w = doc.GetElement(new ElementId(wallIntId)) as Wall;

            double volume = 0;

            GeometryElement geo = w.get_Geometry(uiapp.Application.Create.NewGeometryOptions());

            foreach (GeometryObject obj in geo)
            {
                 Solid solid = obj as Solid;

                 if (solid != null)
                 {
                        volume += solid.Volume;
                 }
            }

            try
            {
                TaskDialog.Show("Revit", w.get_Parameter("Volume").AsDouble().ToString());
            }
            catch (Exception e)
            {
                TaskDialog.Show("Revit", e.ToString());
            }

            return volume;
        }

        public void SplineWall(IList<XYZ> points)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a new wall with spline"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                HermiteSpline wallPath = HermiteSpline.Create(points, false);

                Level level = FindAndSortLevels(doc).Last();

                Element wall = Wall.Create(doc, wallPath, level.Id, false);

                t.Commit();
            }
        }

        public void SetFinishFace(int id, int property)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Set wall finish face to interior"))
            {

                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                ElementId wallId = new ElementId(id);

                Wall w = doc.GetElement(wallId) as Wall;

                w.get_Parameter(BuiltInParameter.WALL_KEY_REF_PARAM).Set(property);

                t.Commit();
            }
        }

        //Insert a door in the host with absolute positions
        public int InsertDoor(double P0Coordx, double P0Coordy, double P0Coordz, int hostId, int familyId)
        {

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a door"))
            {

                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                XYZ location = new XYZ(P0Coordx, P0Coordy, P0Coordz);

                ElementId wallId = new ElementId(hostId);

                Element wall = doc.GetElement(wallId);

                FamilySymbol symbol = null;

                if (familyId == 0)
                {

                    symbol = GetFirstSymbol(FindDoorFamilies(doc).FirstOrDefault());

                }
                else {
                    symbol = doc.GetElement(new ElementId(familyId)) as FamilySymbol;
                }

                FamilyInstance door = doc.Create.NewFamilyInstance(location, symbol, wall, wall.Document.GetElement(wall.LevelId) as Level, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                door.flipFacing();
                doc.Regenerate();
                door.flipFacing();
                doc.Regenerate();
                door.flipFacing();
                doc.Regenerate();

                t.Commit();

                return door.Id.IntegerValue;
            }
        }

        //Insert a door in the host with relative position. 
        public int InsertDoor1(double deltaFromStart, double deltaFromGround, int hostId, int familyId)
        {

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a door"))
            {

                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                ElementId wallId = new ElementId(hostId);

                Element wall = doc.GetElement(wallId);

                LocationCurve locAux = wall.Location as LocationCurve;

                XYZ start = locAux.Curve.GetEndPoint(0);

                XYZ dir = locAux.Curve.GetEndPoint(1) - start;

                XYZ location = start + dir.Normalize() * deltaFromStart;

                FamilySymbol symbol = null;

                if (familyId == 0)
                {

                    symbol = GetFirstSymbol(FindDoorFamilies(doc).FirstOrDefault());

                }
                else
                {
                    symbol = doc.GetElement(new ElementId(familyId)) as FamilySymbol;
                }

                FamilyInstance door = doc.Create.NewFamilyInstance(location, symbol, wall, wall.Document.GetElement(wall.LevelId) as Level, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                //door.get_Parameter("Sill Height").Set(deltaFromGround);

                door.flipFacing();
                doc.Regenerate();
                door.flipFacing();
                doc.Regenerate();
                door.flipFacing();
                doc.Regenerate();

                t.Commit();

                return door.Id.IntegerValue;
            }
        }

        public int InsertWindow(double deltaFromStart, double deltaFromGround, int hostId)
        {

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a window"))
            {

                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                ElementId wallId = new ElementId(hostId);

                Element wall = doc.GetElement(wallId);

                LocationCurve locAux = wall.Location as LocationCurve;

                XYZ start = locAux.Curve.GetEndPoint(0);

                XYZ dir = locAux.Curve.GetEndPoint(1) - start;

                XYZ location = start + dir.Normalize() * deltaFromStart;

                FamilySymbol symbol = GetFirstSymbol(FindWindowFamilies(doc).FirstOrDefault());

                FamilyInstance door = doc.Create.NewFamilyInstance(location, symbol, wall, wall.Document.GetElement(wall.LevelId) as Level, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                door.get_Parameter("Sill Height").Set(deltaFromGround);

                t.Commit();

                return door.Id.IntegerValue;
            }
        }

        public int CreateLevel(double h)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a level"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                int id = 0;

                double elevation = (double)h;

                Level level = null;
                foreach (Level l in FindAndSortLevels(doc))
                {
                    if (l.Elevation == elevation)
                    {
                        level = l;
                        break;
                    }
                }

                if (level == null)
                {
                    level = doc.Create.NewLevel(elevation);

                    level.Name = "Level " + levelCounter;

                    levelCounter++;

                    if (level == null)
                    {
                        TaskDialog.Show("Revit", "Could not create the new level");
                    }

                    else
                    {
                        IEnumerable<ViewFamilyType> viewFamilyTypes = from elem in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                                                                      let type = elem as ViewFamilyType
                                                                      where type.ViewFamily == ViewFamily.FloorPlan
                                                                      select type;

                        ViewPlan floorView = ViewPlan.Create(doc, viewFamilyTypes.First().Id, level.Id);

                        viewFamilyTypes = from elem in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                                          let type = elem as ViewFamilyType
                                          where type.ViewFamily == ViewFamily.CeilingPlan
                                          select type;

                        ViewPlan ceilingView = ViewPlan.Create(doc, viewFamilyTypes.First().Id, level.Id);

                        id = level.Id.IntegerValue;
                    }
                }
                else
                {
                    id = level.Id.IntegerValue;
                }

                t.Commit();

                return id;
            }
        }

        public int UpperLevel(int currentLevelId, double elevationToAdd)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a level"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                int id = 0;

                ElementId currentLevelElId = new ElementId(currentLevelId);

                Level currentLevel = doc.GetElement(currentLevelElId) as Level;

                Level upperLevel = null;

                double upperElevation = currentLevel.Elevation + elevationToAdd;

                foreach (Level l in FindAndSortLevels(doc))
                {
                    if (l.Elevation == upperElevation)
                    {
                        upperLevel = l;
                        break;
                    }
                }

                if (upperLevel == null)
                {
                    upperLevel = doc.Create.NewLevel(upperElevation);

                    upperLevel.Name = "Level " + levelCounter;

                    levelCounter++;

                    if (upperLevel == null)
                    {
                        TaskDialog.Show("Revit", "Could not create the new level");
                    }

                    else
                    {
                        IEnumerable<ViewFamilyType> viewFamilyTypes = from elem in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                                                                      let type = elem as ViewFamilyType
                                                                      where type.ViewFamily == ViewFamily.FloorPlan
                                                                      select type;

                        ViewPlan floorView = ViewPlan.Create(doc, viewFamilyTypes.First().Id, upperLevel.Id);

                        viewFamilyTypes = from elem in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                                          let type = elem as ViewFamilyType
                                          where type.ViewFamily == ViewFamily.CeilingPlan
                                          select type;

                        ViewPlan ceilingView = ViewPlan.Create(doc, viewFamilyTypes.First().Id, upperLevel.Id);

                        id = upperLevel.Id.IntegerValue;
                    }
                }
                else
                {
                    id = upperLevel.Id.IntegerValue;
                }

                t.Commit();

                return id;
            }
        }

        public int GetLevel(double height, string name)
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            int id = 0;

            Level level = null;

            foreach (Level l in FindAndSortLevels(doc))
            {
                if (l.Name.Equals(name) && l.Elevation.Equals(height))
                {
                    level = l;
                    break;
                }
            }

            if(level == null)
            {
                TaskDialog.Show("Revit", "Invalid level");
            }
            else
            {
                id = level.Id.IntegerValue;
            }

            return id;
        }

        public int GetLevelByName(string name)
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            int id = 0;

            Level level = null;

            foreach (Level l in FindAndSortLevels(doc))
            {
                if (l.Name.Equals(name))
                {
                    level = l;
                    break;
                }
            }

            if (level == null)
            {
                TaskDialog.Show("Revit", "Invalid level");
            }
            else
            {
                id = level.Id.IntegerValue;
            }

            return id;
        }

        public double GetLevelElevation(int levelID)
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            double elevation = 0;

            Level level = doc.GetElement(new ElementId(levelID)) as Level;

            elevation = level.Elevation;

            return elevation;
        }

        public List<RosettaApp.MyLevel> GetLevelsInfo()
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            IOrderedEnumerable<Level> allLevels = FindAndSortLevels(doc);

            List<RosettaApp.MyLevel> result = new List<RosettaApp.MyLevel>();

            foreach (Level l in allLevels)
            {
                result.Add(new RosettaApp.MyLevel(l.Name, l.Elevation));
            }

            return result;
        }

        public void DeleteLevel(string name)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Deleting level"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                Level level = null;

                foreach (Level l in FindAndSortLevels(doc))
                {
                    if (l.Name.Equals(name))
                    {
                        level = l;
                        break;
                    }
                }
                int id = 0;

                if (level == null)
                {
                    //TaskDialog.Show("Revit", "Invalid level");
                }
                else
                {
                    uiapp.ActiveUIDocument.Document.Delete(level.Id);
                }

                t.Commit();
            }
        } 

        public int CreateRoundFloor(double centerX, double centerY, double centerZ, double radius, int levelId)
        {

             using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a round floor"))
            {
                 t.Start();

                 FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                 failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                 t.SetFailureHandlingOptions(failOp);

                 Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                 Level level = null;

                 ElementId levelIdEl = new ElementId(levelId);

                 level = doc.GetElement(levelIdEl) as Level;

                 int id = 0;

                 if (level == null)
                 {
                     TaskDialog.Show("Revit", "Invalid level");
                 }
                 else
                 {
                     FloorType floorType = new FilteredElementCollector(doc).OfClass(typeof(FloorType)).First() as FloorType;

                     XYZ origin = new XYZ(centerX, centerY, centerZ);

                     Plane plane = new Plane(XYZ.BasisZ, origin);  //XY plane
                     Arc arc1 = Arc.Create(plane, radius, 0, Math.PI);
                     Arc arc2 = Arc.Create(plane, radius, Math.PI, Math.PI * 2);
                     Arc arc3 = Arc.Create(plane, 5, 0, Math.PI);
                     Arc arc4 = Arc.Create(plane, 5, Math.PI, Math.PI * 2);

                     CurveArray profile = new CurveArray();
                     profile.Append(arc1);
                     profile.Append(arc2);

                     CurveArray profile1 = new CurveArray();
                     profile1.Append(arc3);
                     profile1.Append(arc4);

                     //doc.Create.NewOpening(floor, profile1, false);

                     Floor floor = doc.Create.NewFloor(profile, floorType, level, false);

                     floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM).Set(0); //Below the level line

                     id = floor.Id.IntegerValue;
                 }


                 t.Commit();

                 return id;
             }
        }

        public int CreateFloor(double c1x, double c1y, double c1z, double c3x, double c3y, double c3z, int levelId)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a square floor"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                Level level = null;

                ElementId levelIdEl = new ElementId(levelId);

                level = doc.GetElement(levelIdEl) as Level;

                int id = 0;

                if(level == null){
                    TaskDialog.Show("Revit", "Invalid level");
                }
                else{

                    FloorType floorType = new FilteredElementCollector(doc).OfClass(typeof (FloorType)).First() as FloorType;
        
                    double c2x = c1x + (c3x - c1x);              
                    double c4y = c1y + (c3y - c1y);

                    XYZ c1 = new XYZ(c1x, c1y, 0);                
                    XYZ c2 = new XYZ(c2x, c1y, 0);              
                    XYZ c3 = new XYZ(c3x, c3y, 0);                
                    XYZ c4 = new XYZ(c1x, c4y, 0);
                
                    CurveArray profile = new CurveArray();
                
                    Line sideA = Line.CreateBound(c1, c2);              
                    Line sideB = Line.CreateBound(c2, c3);             
                    Line sideC = Line.CreateBound(c3, c4);              
                    Line sideD = Line.CreateBound(c4, c1);
                
                    profile.Append(sideA);               
                    profile.Append(sideB);
                    profile.Append(sideC);
                    profile.Append(sideD);

                    //XYZ normal = XYZ.BasisZ;

                    Floor floor = doc.Create.NewFloor(profile, floorType, level, false);

                    floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM).Set(0); //Below the level line

                    id = floor.Id.IntegerValue;
                }

                t.Commit();

                return id;
            }
        }

        public int CreateColumn(double locationX, double locationY, double locationZ, int baseLevelId, int topLevelId, double width, int famID)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a column"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                XYZ location = new XYZ(locationX, locationY, locationZ);

                Level level0 = null;

                Level level1 = null;

                ElementId baseLevelIdEl = new ElementId(baseLevelId);

                level0 = doc.GetElement(baseLevelIdEl) as Level;

                ElementId topLevelIdEl = new ElementId(topLevelId);

                level1 = doc.GetElement(topLevelIdEl) as Level;

                int id = 0;

                if (level0 == null || level1 == null)
                {
                    TaskDialog.Show("Revit", "Invalid level");
                }
                else
                {
                    FamilySymbol symbol = null;

                    if (famID != 0)
                    {
                        symbol = doc.GetElement(new ElementId(famID)) as FamilySymbol;
                    }
                    else
                    {
                        symbol = GetFirstSymbol(FindColumnFamilies(doc).FirstOrDefault());

                        if (width != 0)
                        {
                            symbol.get_Parameter("Width").Set(width);
                            symbol.get_Parameter("Depth").Set(width);
                        }
                    }

                    FamilyInstance col = doc.Create.NewFamilyInstance(location, symbol, level0, Autodesk.Revit.DB.Structure.StructuralType.Column);

                    col.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM).Set(level1.Id);
                    col.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM).Set(0.0);
                    col.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM).Set(0.0);

                    id = col.Id.IntegerValue;
                }

                t.Commit();

                return id;
            }
        }

        public int CreateColumnPoints(double p0x, double p0y, double p0z, double p1x, double p1y, double p1z, int baseLevelId, int topLevelId, double width, int famId)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a column"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                XYZ p0 = new XYZ(p0x, p0y, p0z);
                XYZ p1 = new XYZ(p1x, p1y, p1z);

                Level level0 = null;

                Level level1 = null;

                ElementId baseLevelIdEl = new ElementId(baseLevelId);

                level0 = doc.GetElement(baseLevelIdEl) as Level;

                ElementId topLevelIdEl = new ElementId(topLevelId);

                level1 = doc.GetElement(topLevelIdEl) as Level;

                int id = 0;

                if (level0 == null || level1 == null)
                {
                    TaskDialog.Show("Revit", "Invalid level");
                }
                else
                {
                    FamilySymbol symbol = null;

                    if (famId != 0)
                    {
                        symbol = doc.GetElement(new ElementId(famId)) as FamilySymbol;
                    }
                    else
                    {
                        symbol = GetFirstSymbol(FindStructuralColumnFamilies(doc).FirstOrDefault());

                        if (width != 0)
                        {
                            symbol.get_Parameter("Width").Set(width);
                            symbol.get_Parameter("Depth").Set(width);
                        }
                    }

                    FamilyInstance col = doc.Create.NewFamilyInstance(p0, symbol, level0, Autodesk.Revit.DB.Structure.StructuralType.Column);
                    
                    col.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM).Set(level1.Id);
                    col.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM).Set(0.0);
                    col.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM).Set(0.0);

                    col.get_Parameter(BuiltInParameter.SLANTED_COLUMN_TYPE_PARAM).Set(2);

                    LocationCurve lc = col.Location as LocationCurve;
                
                    Curve nline = Line.CreateBound(p0, p1) as Curve;
                    lc.Curve = nline;
                    
                    id = col.Id.IntegerValue;
                }

                t.Commit();

                return id;
            }
        }
        public int CreateBeam(double p0x, double p0y, double p0z, double p1x, double p1y, double p1z, int idInt)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a Beam"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                int id = 0;

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;


                //IEnumerable<Family> familyList = new FilteredElementCollector(doc).OfClass(typeof(Family)).Cast<Family>().Where(e => e.FamilyCategory != null && e.FamilyCategory.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralFraming && e.Name.Equals("M_Glulam-Southern Pine"));

                
                //Family family = null;
                // try to load family
                /*if (familyList.Count() == 0)
                {
                    if (!doc.LoadFamily(fileName, out family))
                    {
                            throw new Exception("Unable to load " + fileName);
                    }
                }
                else
                {
                    family = familyList.First();
                }*/

                FamilySymbol beamSymb = null;

                if (idInt == 0)
                {
                    Family defaultBeamFam = new FilteredElementCollector(doc).OfClass(typeof(Family)).Cast<Family>().Where(e => e.FamilyCategory != null && e.FamilyCategory.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralFraming).First();
                    beamSymb = doc.GetElement(defaultBeamFam.GetFamilySymbolIds().First()) as FamilySymbol;
                }
                else
                {
                    beamSymb = doc.GetElement(new ElementId(idInt)) as FamilySymbol;
                }


                XYZ p0 = new XYZ(p0x, p0y, p0z);
                XYZ p1 = new XYZ(p1x, p1y, p1z);

                try
                {
                    FamilyInstance beam = doc.Create.NewFamilyInstance(Line.CreateBound(p0, p1), beamSymb, null, Autodesk.Revit.DB.Structure.StructuralType.Beam);

                    id = beam.Id.IntegerValue;
                }
                catch (Exception e)
                {
                    TaskDialog.Show("Revit", e.ToString());
                }

                t.Commit();

                return id;
            }
        }
        public int CreateRoof(IList<XYZ> points, int levelId, int famID)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a Roof"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                Level level = null;

                int id = 0;

                ElementId levelIdEl = new ElementId(levelId);

                level = doc.GetElement(levelIdEl) as Level;

                if (level == null)
                {
                    TaskDialog.Show("Revit", "Invalid level");
                }
                else
                {
                    RoofType roofType = null;

                    if (famID != 0)
                    {
                        roofType = doc.GetElement(new ElementId(famID)) as RoofType;
                    }
                    else
                    {
                        List<Element> roofTypeList = new FilteredElementCollector(doc).OfClass(typeof(RoofType)).ToList();
                        foreach (Element el in roofTypeList)
                        {
                            if (el.Name.Equals("Generic - 125mm") || el.Name.Equals("Generic Roof - 300mm"))
                            {
                                roofType = el as RoofType;
                                break;
                            }
                        }
                        if (roofType == null)
                        {
                            roofType = roofTypeList.First() as RoofType;
                        }
                    }

                    CurveArray profile = new CurveArray();

                    for (int i = 0; i < points.Count - 1; i++)
                    {
                        profile.Append(Line.CreateBound(points[i], points[i + 1]));

                    }

                    ModelCurveArray curveArray = new ModelCurveArray();

                    
                    FootPrintRoof roof = doc.Create.NewFootPrintRoof(profile, level, roofType, out curveArray);

                    id = roof.Id.IntegerValue;
                           
                }

                t.Commit();

                return id;
            }
        }

        public int CreateFloorFromPoints(IList<XYZ> points, int levelId, int famID)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a floor from points"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                Level level = null;

                int id = 0;

                ElementId levelIdEl = new ElementId(levelId);

                level = doc.GetElement(levelIdEl) as Level;

                if (level == null)
                {
                    TaskDialog.Show("Revit", "Invalid level");
                }
                else
                {

                    FloorType floorType = null;
                    if (famID != 0)
                    {
                        floorType = doc.GetElement(new ElementId(famID)) as FloorType;
                    }
                    else
                    {
                        floorType = new FilteredElementCollector(doc).OfClass(typeof(FloorType)).First() as FloorType;
                    }

                    CurveArray profile = new CurveArray();

                    for (int i = 0; i < points.Count - 1 ; i++)
                    {
                        profile.Append(Line.CreateBound(points[i], points[i + 1]));

                    }

                    Floor floor = doc.Create.NewFloor(profile, floorType, level, false);

                    floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM).Set(0); //Below the level line

                    id = floor.Id.IntegerValue;

                }
                t.Commit();

                return id;
            }
        }

        public void CreateOpeningInFloor(double c1x, double c1y, double c1z, double c3x, double c3y, double c3z, int floorId)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a square floor opening"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                Element floor = doc.GetElement(new ElementId(floorId));

                double c2x = c1x + (c3x - c1x);

                double c4y = c1y + (c3y - c1y);

                XYZ c1 = new XYZ(c1x, c1y, c1z);
                XYZ c2 = new XYZ(c2x, c1y, c1z);
                XYZ c3 = new XYZ(c3x, c3y, c3z);
                XYZ c4 = new XYZ(c1x, c4y, c3z);

                CurveArray profile = new CurveArray();

                Line sideA = Line.CreateBound(c1, c2);
                Line sideB = Line.CreateBound(c2, c3);
                Line sideC = Line.CreateBound(c3, c4);
                Line sideD = Line.CreateBound(c4, c1);

                profile.Append(sideA);
                profile.Append(sideB);
                profile.Append(sideC);
                profile.Append(sideD);

                doc.Create.NewOpening(floor, profile, false);

                t.Commit();
            }

        }

        public void CreateHoleInSlab(IList<XYZ> points, int slabId)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Creating a hole in a slab"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                Element floor = doc.GetElement(new ElementId(slabId));

                CurveArray profile = new CurveArray();

                for (int i = 0; i < points.Count - 1; i++)
                {
                    profile.Append(Line.CreateBound(points[i], points[i + 1]));

                }

                doc.Create.NewOpening(floor, profile, false);

                t.Commit();
            }
        }

        public int CreateStairsRun(int bottomLevelId, int topLevelId, double p0x, double p0y, double p0z, 
                                                                        double p1x, double p1y, double p1z,
                                                                        double width, int famId)
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            int id = 0;

            using (StairsEditScope newStairsScope = new StairsEditScope(doc, "New Stairs Run"))
            {
                double mirrorDefaultHight = 0.5905512;

                Level levelBottom = null;

                Level levelTop = null;

                ElementId bottomLevelIdEl = new ElementId(bottomLevelId);

                levelBottom = doc.GetElement(bottomLevelIdEl) as Level;

                ElementId topLevelIdEl = new ElementId(topLevelId);

                levelTop = doc.GetElement(topLevelIdEl) as Level;


                if (levelBottom == null || levelTop == null)
                {
                    TaskDialog.Show("Revit", "Invalid level");
                }
                else
                {
                    ElementId newStairsId = newStairsScope.Start(levelBottom.Id, levelTop.Id);

                    using (Transaction t = new Transaction(doc, "Create a new stairs run"))
                    {
                        t.Start();

                        XYZ bottomP0 = new XYZ(p0x, p0y, p0z);
                        XYZ bottomP1 = new XYZ(p1x, p1y, p1z);

                        Line locationLine = Line.CreateBound(bottomP0, bottomP1);

                        try
                        {
                            StairsRun newRun = StairsRun.CreateStraightRun(doc, newStairsId, locationLine, StairsRunJustification.Center);
                            newRun.TopElevation = levelTop.Elevation;

                            if (width != 0)
                            {
                                newRun.ActualRunWidth = width;
                            }
                        }
                        catch (Exception e)
                        {
                            TaskDialog.Show("Revit", e.ToString());
                        }

                        t.Commit();
                    }

                    newStairsScope.Commit(new StairsFailurePreprocessor());

                    id = newStairsId.IntegerValue;
                }

                return id;
            }
        }

        public int CreateStairsRun(int bottomLevelInt, int topLevelInt, double bpx, double bpy, double bpz, double tpx, double tpy, double tpz)
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            int id = 0;

            using (StairsEditScope newStairsScope = new StairsEditScope(doc, "New Stairs Run"))
            {

                Level levelBottom = null;

                Level levelTop = null;

                ElementId bottomLevelIdEl = new ElementId(bottomLevelInt);

                levelBottom = doc.GetElement(bottomLevelIdEl) as Level;

                ElementId topLevelIdEl = new ElementId(topLevelInt);

                levelTop = doc.GetElement(topLevelIdEl) as Level;

                if (levelBottom == null || levelTop == null)
                {
                    TaskDialog.Show("Revit", "Invalid level");
                }
                else
                {
                    ElementId newStairsId = newStairsScope.Start(levelBottom.Id, levelTop.Id);

                    using (Transaction t = new Transaction(doc, "Create a new stairs run"))
                    {
                        t.Start();

                        double mirrorDefaultHeight = 0.5905512;
                        double blanketDefaultLenght = 0.82021;

                        XYZ bottomP0 = new XYZ(bpx, bpy, bpz);
                        
                        XYZ topP0 = new XYZ(tpx, tpy, bpz);

                        int riserNum = (int)Math.Floor((levelTop.Elevation - levelBottom.Elevation) / mirrorDefaultHeight);

                        double stairLenght = riserNum * blanketDefaultLenght; 

                        Line locationLine = Line.CreateBound(bottomP0, new XYZ(0, stairLenght, 0));

                        StairsRun newRun = StairsRun.CreateStraightRun(doc, newStairsId, locationLine, StairsRunJustification.Center);

                        t.Commit();
                    }

                    newStairsScope.Commit(new StairsFailurePreprocessor());

                    id = newStairsId.IntegerValue;
                }

                return id;
            }
        }

        //Does not work outside initial creation scope
        public int CreateStairLanding(double bottomleftcornerx, double bottomleftcornery, double bottomleftcornerz,
                                      double topleftcornerx, double topleftcornery, double topleftcornerz,
                                      double bottomrightcornerx, double bottomrightcornery, double bottomrightcornerz,
                                      double toprightcornerx, double toprightcornery, double toprightcornerz,
                                      int stairsId)
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            int id = 0;

            using (StairsEditScope newStairsScope = new StairsEditScope(doc, "New Stairs landing"))
            {
                XYZ bottomLeftCorner = new XYZ(bottomleftcornerx, bottomleftcornery, bottomleftcornerz);
                XYZ topLeftCorner = new XYZ(topleftcornerx, topleftcornery, topleftcornerz);
                XYZ bottomRightCorner = new XYZ(bottomrightcornerx, bottomrightcornery, bottomrightcornerz);
                XYZ topRightCorner = new XYZ(toprightcornerx, toprightcornery, toprightcornerz);

                ElementId stairRunId = new ElementId(stairsId);

                Stairs sts = (Stairs)doc.GetElement(stairRunId);
                
                using (Transaction t = new Transaction(doc, "Create a new stairs landing"))
                {
                    t.Start();

                    CurveLoop landingLoop = new CurveLoop();

                    Line l1 = Line.CreateBound(bottomLeftCorner, bottomRightCorner);
                    Line l2 = Line.CreateBound(bottomRightCorner, topRightCorner);
                    Line l3 = Line.CreateBound(topRightCorner, topLeftCorner);
                    Line l4 = Line.CreateBound(topLeftCorner, bottomLeftCorner);

                    landingLoop.Append(l1);
                    landingLoop.Append(l2);
                    landingLoop.Append(l3);
                    landingLoop.Append(l4);

                    StairsRun run = (StairsRun)doc.GetElement(sts.GetStairsRuns().First());

                    StairsLanding landing = null;
                    try
                    {
                        landing = StairsLanding.CreateSketchedLanding(doc, sts.Id, landingLoop, run.TopElevation);
                    }

                    catch (Exception e)
                    {
                        TaskDialog.Show("Revit", e.ToString());
                    }

                    id = landing.Id.IntegerValue;

                    t.Commit();
                }

                newStairsScope.Commit(new StairsFailurePreprocessor());
            }

            return id;
        }

        public int CreateTopoSurface(IList<double> coordinates)
        {
            int id = 0;

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Create a topoSurface"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                IList<XYZ> pts = new List<XYZ>();

                for (int i = 0; i < coordinates.Count; i = i + 3)
                {
                    XYZ p = new XYZ(coordinates[i], coordinates[i + 1], coordinates[i + 2]);

                    pts.Add(p);
                }

                TopographySurface surface = TopographySurface.Create(doc, pts);

                id = surface.Id.IntegerValue;

                t.Commit();
            }

            return id;
        }

        public int CreateBuildingPad(IList<double> coordinates, int levelIdInt)
        {
            int id = 0;

            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Create a Building Pad"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                ElementId buildingPadType = new FilteredElementCollector(doc).OfClass(typeof(BuildingPadType)).ToElementIds().First();

                ElementId levelId = new ElementId(levelIdInt);

                IList<XYZ> pts = new List<XYZ>();

                for (int i = 0; i < coordinates.Count; i = i + 3)
                {
                    XYZ p = new XYZ(coordinates[i], coordinates[i + 1], coordinates[i + 2]);

                    pts.Add(p);
                }

                CurveLoop curves = new CurveLoop();
                List<CurveLoop> curveloops = new List<CurveLoop>();

                for (int i = 0; i < pts.Count - 1; i++) 
                {
                    Line l = Line.CreateBound(pts[i], pts[i + 1]);
                    curves.Append(l);
                }

                curveloops.Add(curves);
                try
                {
                    BuildingPad bp = BuildingPad.Create(doc, buildingPadType, levelId, curveloops);

                    id = bp.Id.IntegerValue;

                }

                catch (Exception e)
                {
                    TaskDialog.Show("Revit", e.ToString());
                }
                t.Commit();
            }

            return id;
        }

        public void CreateRailings(int slabid)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Create a railing"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                ElementId hostID = new ElementId(slabid); 

                ElementId railType = new FilteredElementCollector(doc).OfClass(typeof(RailingType)).ToElementIds().First();

                try
                {
                    Railing.Create(doc, hostID, railType, RailingPlacementPosition.Stringer);
                }
                catch (Exception e)
                {
                    TaskDialog.Show("Revit", e.ToString());
                }
                t.Commit();
            }
        }

        public void HighlightElement(int idInt)
        {
            Autodesk.Revit.UI.UIDocument UIdoc = uiapp.ActiveUIDocument;

            Autodesk.Revit.UI.Selection.Selection selElements = UIdoc.Selection;

            ElementId el = new ElementId(idInt);

            IList<ElementId> listIds = new List<ElementId>();

            listIds.Add(el);

            selElements.SetElementIds(listIds);
        }

        public IList<int> GetSelectedElements()
        {
            IList<int> ids = new List<int>();

            Autodesk.Revit.UI.UIDocument UIdoc = uiapp.ActiveUIDocument;

            Autodesk.Revit.UI.Selection.Selection selElements = UIdoc.Selection;

            foreach (ElementId el in selElements.GetElementIds())
            {
                ids.Add(el.IntegerValue);
            }

            return ids;
        }

        public void ImportDWG(String fileName)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Import DWG file"))
            {
                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                TaskDialog.Show("Revit", fileName);

                try
                {
                    DWGImportOptions dwgImportOptions = new DWGImportOptions();

                    View view = doc.ActiveView;

                    ElementId eID = null;

                    doc.Import(fileName, dwgImportOptions, view, out eID);
                }
                catch (Exception e)
                {
                    TaskDialog.Show("Revit", e.ToString());
                }

                t.Commit();
            }
        }

        public int LoadFamilyFunc(string fileName)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Load Family"))
            {
                t.Start();

                int id = 0;

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                Family family = null;
                // try to load family
                
                if (!doc.LoadFamily(fileName, out family))
                {
                    TaskDialog.Show("Revit", "It was not possible to load the desired family");
                    t.Commit();
                    return id;
                }
              
                //ElementId symbId = family.GetFamilySymbolIds().First();

                id = family.Id.IntegerValue;
                
                t.Commit();

                return id;
            }
        }

        public int FamilyElementFunc(int familyID, bool flag, IList<String> namesList, IList<double> valuesList)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Select Family Element"))
            {
                t.Start();

                int id = 0;

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                Family family = doc.GetElement(new ElementId(familyID)) as Family;

                ElementId symbId = null;

                if (namesList.Count == 0 || namesList.Count != valuesList.Count)
                {
                    symbId = family.GetFamilySymbolIds().First();

                }
                else
                {
                    foreach (ElementId instID in family.GetFamilySymbolIds())
                    {
                        bool test = true;
                        double epsilon = 0.022;

                        FamilySymbol testSymb = doc.GetElement(instID) as FamilySymbol;

                        for (int i = 0; i < namesList.Count; i++)
                        {
                            double valueTest = testSymb.get_Parameter(namesList[i]).AsDouble();

                            if (!(Math.Abs(valueTest - valuesList[i]) < epsilon))
                            {
                                test = false;
                                break;
                            }
                        }

                        if (test)
                        {
                            symbId = instID;
                            break;
                        }
                    }

                    if (symbId == null)
                    {
                        if (flag)
                        {
                            symbId = family.GetFamilySymbolIds().First();
                            FamilySymbol oldt = doc.GetElement(symbId) as FamilySymbol;
                            String nName = "CustomFamily" + customFamilyCounter.ToString();
                            customFamilyCounter++;
                            FamilySymbol nt = oldt.Duplicate(nName) as FamilySymbol;

                            for (int i = 0; i < namesList.Count; i++)
                            {
                                nt.get_Parameter(namesList[i]).Set(valuesList[i]);
                            }

                            symbId = nt.Id;
                        }
                        else
                        {
                            symbId = family.GetFamilySymbolIds().First();
                        }
                    }
                }

                id = symbId.IntegerValue;

                t.Commit();

                return id;
            }
        }
        public bool IsProject()
        {
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            return !doc.IsFamilyDocument;
        }

        public int IntersectWallFloor(int wallId, int floorId)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Intersect wall with floor"))
            {
                t.Start();

                FailureHandlingOptions failOp = t.GetFailureHandlingOptions();
                failOp.SetFailuresPreprocessor(new OffAxisWarningSwallower());
                t.SetFailureHandlingOptions(failOp);

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                int id = wallId;

                bool begInside;

                bool endInside;

                Floor floor = (Floor)doc.GetElement(new ElementId(floorId));

                Wall wall = (Wall)doc.GetElement(new ElementId(wallId));

                LocationCurve l1 = wall.Location as LocationCurve;

                Curve wallCurve = l1.Curve;

                XYZ beg = wallCurve.GetEndPoint(0);

                XYZ end = wallCurve.GetEndPoint(1);

                XYZ newBeg = beg;

                XYZ newEnd = end;

                double epsilon = 0.00001;

                List<List<XYZ>> polygon = new List<List<XYZ>>();

                GeometryElement floorGeo = floor.get_Geometry(uiapp.Application.Create.NewGeometryOptions());

                try
                {
                    foreach (GeometryObject obj in floorGeo)
                    {
                        Solid solid = obj as Solid;

                        if (solid != null)
                        {
                            GetBoundary(polygon, solid);
                        }
                    }

                    List<XYZ> intersections = new List<XYZ>();
                    List<XYZ> intersections2 = new List<XYZ>();
                    List<XYZ> intersections3 = new List<XYZ>();

                    XYZ q = new XYZ(double.MinValue, double.MinValue, double.MinValue);

                    foreach (List<XYZ> pts in polygon)
                    {

                        XYZ r = null;

                        for (int i = 0; i < pts.Count - 1; i++)
                        {
                            if (pts[i].X > q.X)
                            {
                                q = pts[i];
                            }

                            r = intersect(pts[i], pts[i + 1], newBeg, newEnd);

                            if (r != null)
                            {
                                bool contain = false;
                                foreach (XYZ inter in intersections)
                                {
                                    contain = (Math.Abs(inter.X - r.X) < epsilon && Math.Abs(inter.Y - r.Y) < epsilon);
                                    if (contain)
                                    {
                                        break;
                                    }
                                }
                                if (!contain)
                                {
                                    intersections.Add(r);
                                }
                            }

                        }

                        r = intersect(pts[0], pts[pts.Count - 1], newBeg, newEnd);

                        if (r != null)
                        {
                            bool contain = false;
                            foreach (XYZ inter in intersections)
                            {
                                contain = (Math.Abs(inter.X - r.X) < epsilon && Math.Abs(inter.Y - r.Y) < epsilon);
                                if (contain)
                                {
                                    break;
                                }
                            }
                            if (!contain)
                            {
                                intersections.Add(r);
                            }
                        }

                    }

                    q = new XYZ(q.X + 1, q.Y, newBeg.Z);

                    foreach (List<XYZ> pts in polygon)
                    {

                        XYZ r = null;

                        for (int i = 0; i < pts.Count - 1; i++)
                        {

                            r = intersect(pts[i], pts[i + 1], newBeg, q);

                            if (r != null)
                            {
                                bool contain = false;
                                foreach (XYZ inter in intersections2)
                                {
                                    contain = (Math.Abs(inter.X - r.X) < epsilon && Math.Abs(inter.Y - r.Y) < epsilon);
                                    if (contain)
                                    {
                                        break;
                                    }
                                }
                                if (!contain)
                                {
                                    intersections2.Add(r);
                                }
                            }
                        }


                        r = intersect(pts[0], pts[pts.Count - 1], newBeg, q);

                        if (r != null)
                        {
                            bool contain = false;
                            foreach (XYZ inter in intersections2)
                            {
                                contain = (Math.Abs(inter.X - r.X) < epsilon && Math.Abs(inter.Y - r.Y) < epsilon);
                                if (contain)
                                {
                                    break;
                                }
                            }
                            if (!contain)
                            {
                                intersections2.Add(r);
                            }
                        }

                    }

                    if (intersections2.Count % 2 == 0)
                    {
                        begInside = false;
                    }
                    else
                    {
                        begInside = true;
                    }

                    foreach (List<XYZ> pts in polygon)
                    {

                        XYZ r = null;

                        for (int i = 0; i < pts.Count - 1; i++)
                        {

                            r = intersect(pts[i], pts[i + 1], newEnd, q);

                            if (r != null)
                            {
                                bool contain = false;
                                foreach (XYZ inter in intersections3)
                                {
                                    contain = (Math.Abs(inter.X - r.X) < epsilon && Math.Abs(inter.Y - r.Y) < epsilon);
                                    if (contain)
                                    {
                                        break;
                                    }
                                }
                                if (!contain)
                                {
                                    intersections3.Add(r);
                                }
                            }
                        }


                        r = intersect(pts[0], pts[pts.Count - 1], newEnd, q);

                        if (r != null)
                        {
                            bool contain = false;
                            foreach (XYZ inter in intersections3)
                            {
                                contain = (Math.Abs(inter.X - r.X) < epsilon && Math.Abs(inter.Y - r.Y) < epsilon);
                                if (contain)
                                {
                                    break;
                                }
                            }
                            if (!contain)
                            {
                                intersections3.Add(r);
                            }
                        }

                    }

                    if (intersections3.Count % 2 == 0)
                    {
                        endInside = false;
                    }
                    else
                    {
                        endInside = true;
                    }

                    if (intersections.Count == 0 && begInside == false && endInside == false)
                    {
                        doc.Delete(new ElementId(wallId));
                        id = 0;
                    }
                    else
                    {
                        foreach (XYZ pt in intersections)
                        {
                            double distBeg = Line.CreateBound(newBeg, pt).Length;
                            double distEnd = Line.CreateBound(newEnd, pt).Length;

                            if (begInside == false && (distBeg <= distEnd || endInside))
                            {
                                newBeg = pt;
                                begInside = true;

                            }
                            else if (endInside == false && (distEnd <= distBeg || begInside))
                            {
                                newEnd = pt;
                                endInside = true;
                            }
                        }
                    }

                    if ((newBeg.X != beg.X || newBeg.Y != beg.Y) || (newEnd.X != end.X || newEnd.Y != end.Y))
                    {


                        ElementId level0Id = wall.LevelId;
                        ElementId level1Id = wall.get_Parameter("Top Constraint").AsElementId();

                        XYZ newLineBeg = new XYZ(newBeg.X, newBeg.Y, beg.Z);
                        XYZ newLineEnd = new XYZ(newEnd.X, newEnd.Y, end.Z);
                        doc.Delete(new ElementId(wallId));
                        id = 0;

                        Line line = Line.CreateBound(newLineBeg, newLineEnd);
                        Element newWall = Wall.Create(doc, line, level0Id, false);

                        newWall.get_Parameter("Top Constraint").Set(level1Id);

                        id = newWall.Id.IntegerValue;
                    }
                }
                catch (Exception e)
                {
                    TaskDialog.Show("Revit", e.ToString());
                }
                t.Commit();

                return id;
            }
        }
        public void DeleteSelectedElement(int id)
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Deleting an element"))
            {

                t.Start();

                ElementId delId = new ElementId(id);

                uiapp.ActiveUIDocument.Document.Delete(delId);

                t.Commit();
            }

        }

        //To implement
        public void DeleteAllElements()
        {
            using (Transaction t = new Transaction(uiapp.ActiveUIDocument.Document, "Deleting All current elements"))
            {

                t.Start();

                Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

                List<Element> elements = new List<Element>();

                FilteredElementCollector collector = new FilteredElementCollector(doc).WhereElementIsNotElementType();

                foreach (Element e in collector)
                {
                    if (null != e.Category && e.Category.HasMaterialQuantities)
                    {
                        elements.Add(e);
                    }
                }

                foreach (Element e in elements)
                {
                    doc.Delete(e.Id);
                }

                t.Commit();
            }

        }
        //Auxiliary functions

        private static IEnumerable<Family> FindDoorFamilies(Autodesk.Revit.DB.Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfClass(typeof(Family))
                .Cast<Family>()
                .Where(e => e.FamilyCategory != null
                        && e.FamilyCategory.Id.IntegerValue == (int)BuiltInCategory.OST_Doors);
        }

        private static IEnumerable<Family> FindWindowFamilies(Autodesk.Revit.DB.Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfClass(typeof(Family))
                .Cast<Family>()
                .Where(e => e.FamilyCategory != null
                        && e.FamilyCategory.Id.IntegerValue == (int)BuiltInCategory.OST_Windows);
        }

        private static IEnumerable<Family> FindColumnFamilies(Autodesk.Revit.DB.Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfClass(typeof(Family))
                .Cast<Family>()
                .Where(e => e.FamilyCategory != null
                        && e.FamilyCategory.Id.IntegerValue == (int)BuiltInCategory.OST_Columns);
        }

        private static IEnumerable<Family> FindStructuralColumnFamilies(Autodesk.Revit.DB.Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfClass(typeof(Family))
                .Cast<Family>()
                .Where(e => e.FamilyCategory != null
                        && e.FamilyCategory.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralColumns);
        }

        private static IEnumerable<Family> FindWallFamilies(Autodesk.Revit.DB.Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfClass(typeof(Family))
                .Cast<Family>()
                .Where(e => e.FamilyCategory != null
                        && e.FamilyCategory.Id.IntegerValue == (int)BuiltInCategory.OST_Walls);
        }

        private static FamilySymbol GetFirstSymbol(Family family)
        {
            return (from FamilySymbol fs in family.Symbols select fs).FirstOrDefault();
        }

        public static IOrderedEnumerable<Level> FindAndSortLevels(Autodesk.Revit.DB.Document doc)
        {
            return new FilteredElementCollector(doc)
                            .WherePasses(new ElementClassFilter(typeof(Level), false))
                            .Cast<Level>()
                            .OrderBy(e => e.Elevation);
        }

        public bool GetBoundary(List<List<XYZ>> polygons, Solid solid)
        {

            double _offset = 0.1;
            PlanarFace lowest = null;
            FaceArray faces = solid.Faces;

            foreach (Face f in faces)
            {
                PlanarFace pf = f as PlanarFace;
                if (null != pf && (pf.Normal.X == 0 && pf.Normal.Y == 0))
                {
                    if ((null == lowest)
                      || (pf.Origin.Z < lowest.Origin.Z))
                    {
                        lowest = pf;
                    }
                }
            }
            if (null != lowest)
            {
                XYZ p, q = XYZ.Zero;
                bool first;
                int i, n;
                EdgeArrayArray loops = lowest.EdgeLoops;
                foreach (EdgeArray loop in loops)
                {
                    List<XYZ> vertices = new List<XYZ>();
                    first = true;
                    foreach (Edge e in loop)
                    {
                        IList<XYZ> points = e.Tessellate();
                        p = points[0];
                        if (!first)
                        {
                            Debug.Assert(p.IsAlmostEqualTo(q),
                              "expected subsequent start point"
                              + " to equal previous end point");
                        }
                        n = points.Count;
                        q = points[n - 1];
                        for (i = 0; i < n - 1; ++i)
                        {
                            XYZ v = points[i];
                            v -= _offset * XYZ.BasisZ;
                            vertices.Add(v);
                        }
                    }
                    q -= _offset * XYZ.BasisZ;
                    Debug.Assert(q.IsAlmostEqualTo(vertices[0]),
                      "expected last end point to equal"
                      + " first start point");
                    polygons.Add(vertices);
                }
            }
            return null != lowest;
        }

        public XYZ intersect(XYZ lb1, XYZ le1, XYZ lb2, XYZ le2)
        {
            XYZ result = null;

            double dx1 = le1.X - lb1.X;
            double dy1 = le1.Y - lb1.Y;

            double dx2 = le2.X - lb2.X;
            double dy2 = le2.Y - lb2.Y;

            double denominator = (dy1 * dx2 - dx1 * dy2);

            double t1 = ((lb1.X - lb2.X) * dy2 + (lb2.Y - lb1.Y) * dx2) / denominator;

            if (double.IsInfinity(t1))
            {
                return null;
            }

            double t2 = ((lb2.X - lb1.X) * dy1 + (lb1.Y - lb2.Y) * dx1) / -denominator;

            result = new XYZ(lb1.X + dx1 * t1, lb1.Y + dy1 * t1, 0);

            if(((t1 >= 0) && (t1 <= 1)) && ((t2 >= 0) && (t2 <= 1)))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        
    }
}
