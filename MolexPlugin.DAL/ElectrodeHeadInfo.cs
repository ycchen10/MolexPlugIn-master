using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NXOpen;
using Basic;
using MolexPlugin.Model;

namespace MolexPlugin.DAL
{
    /// <summary>
    /// 电极头信息
    /// </summary>
    public class ElectrodeHeadInfo
    {
        private CreateConditionModel conditionModel;

        public Point3d CenterPt { get; private set; }

        public Point3d DisPt { get; private set; }
        public ElectrodeHeadInfo(CreateConditionModel model)
        {
            this.conditionModel = model;
            GetHeadAttr();
        }
        /// <summary>
        /// 获取中心点
        /// </summary>
        private void GetHeadAttr()
        {
            Point3d centerPt = new Point3d();
            Point3d disPt = new Point3d();
            Matrix4 matr = this.conditionModel.Work.WorkMatr;
            Matrix4 invers = matr.GetInversMatrix();
            CartesianCoordinateSystem csys = BoundingBoxUtils.CreateCoordinateSystem(matr, invers);//坐标
            BoundingBoxUtils.GetBoundingBoxInLocal(this.conditionModel.Bodys.ToArray(), csys, this.conditionModel.Work.WorkMatr, ref centerPt, ref disPt);
            this.DisPt = disPt;
            this.CenterPt = centerPt;
        }

        /// <summary>
        /// 获取电极矩阵
        /// </summary>
        /// <returns></returns>
        public Matrix4 GetEleMatr()
        {
            double anle = UMathUtils.Angle(this.conditionModel.Work.WorkMatr.GetZAxis(), this.conditionModel.Vec);
            Matrix4 mat;
            if (UMathUtils.IsEqual(anle, 0))
            {
                mat = new Matrix4(this.conditionModel.Work.WorkMatr);
                mat.RolateWithX(Math.PI);
                return mat;
            }
            else
            {
                UVector zVec = new UVector(-this.conditionModel.Vec.X, -this.conditionModel.Vec.Y, -this.conditionModel.Vec.Z);
                UVector xVec = new UVector();
                UVector yVec = new UVector();
                Matrix4 inver = this.conditionModel.Work.WorkMatr.GetInversMatrix();

                UVector orinig = new UVector(CenterPt.X, CenterPt.Y, CenterPt.Z);
                inver.ApplyPos(ref orinig);
                double anle1 = UMathUtils.Angle(this.conditionModel.Work.WorkMatr.GetXAxis(), this.conditionModel.Vec);
                if (UMathUtils.IsEqual(anle1, 0) || UMathUtils.IsEqual(anle1, Math.PI))
                {
                    this.conditionModel.Work.WorkMatr.GetYAxis(ref yVec);
                    xVec = yVec ^ zVec;
                }
                else
                {
                    this.conditionModel.Work.WorkMatr.GetXAxis(ref xVec);
                    yVec = xVec ^ zVec;
                }
                mat = new Matrix4();
                mat.Identity();
                mat.TransformToZAxis(orinig, xVec, yVec);
                return mat;
            }
        }


    }
}
