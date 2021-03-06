﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NXOpen;
using Basic;
using NXOpen.Assemblies;

namespace MolexPlugin.Model
{
    /// <summary>
    /// work
    /// </summary>
    public class WorkModel : AbstractModel, IComparable<WorkModel>
    {
        /// <summary>
        /// work号
        /// </summary>
        public int WorkNumber { get; private set; }
        /// <summary>
        /// 矩阵
        /// </summary>
        public Matrix4 WorkMatr { get; private set; }
        public WorkModel()
        {

        }
        public WorkModel(string filePath,MoldInfoModel moldInfo, int workNumber, Matrix4 matr)
        {
            this.MoldInfo = moldInfo;
            this.WorkNumber = workNumber;
            this.WorkMatr = matr;
            this.PartType = "Work";
            GetAssembleName();
            this.WorkpieceDirectoryPath = filePath;
            this.WorkpiecePath = filePath + this.AssembleName + ".prt";
        }
     
        public override void CreatePart()
        {             
            Part part = PartUtils.NewFile(this.WorkpiecePath) as Part;
            this.PartTag = part;
            SetAttribute();
        }

        public override void GetAssembleName()
        {
            this.AssembleName = this.MoldInfo.MoldNumber + "-" + this.MoldInfo.WorkpieceNumber + "-WORK" + WorkNumber.ToString();
        }

        public override void GetModelForPart(Part part)
        {
            this.GetAttribute(part);
            this.AssembleName = part.Name;
            this.WorkpiecePath = part.FullPath;
            this.WorkpieceDirectoryPath = Path.GetDirectoryName(WorkpiecePath) + "\\";
            this.AssembleName = Path.GetFileNameWithoutExtension(this.WorkpiecePath);
        }

        public override Component Load(Part parentPart)
        {
            Matrix4 matr = new Matrix4();
            matr.Identity();
            return Basic.AssmbliesUtils.PartLoad(parentPart, this.WorkpiecePath, this.AssembleName, matr, new Point3d(0, 0, 0));
        }
        protected override void SetAttribute()
        {
            base.SetAttribute();
            AttributeUtils.AttributeOperation("WorkNumber", this.WorkNumber, this.PartTag);
            AttributeUtils.AttributeOperation("Matrx4", Matrx4ToString(this.WorkMatr), this.PartTag);
        }
        protected override void GetAttribute(Part part)
        {
            base.GetAttribute(part);
            this.WorkNumber = AttributeUtils.GetAttrForInt(part, "WorkNumber");
            string[] temp = new string[4];
            for (int i = 0; i < 4; i++)
            {
                temp[i] = AttributeUtils.GetAttrForString(part, "Matrx4", i);
            }
            this.WorkMatr = StringToMatrx4(temp);
        }
        /// <summary>
        /// 矩阵转字符
        /// </summary>
        /// <param name="matr"></param>
        /// <returns></returns>
        private string[] Matrx4ToString(Matrix4 matr)
        {
            string[] temp = new string[4];

            for (int i = 0; i < 4; i++)
            {
                temp[i] = Math.Round(matr.matrix[i, 0], 4).ToString() + "," + Math.Round(matr.matrix[i, 1], 4).ToString() + "," +
                   Math.Round(matr.matrix[i, 2], 4).ToString() + "," + Math.Round(matr.matrix[i, 3], 4).ToString();
            }
            return temp;
        }
        /// <summary>
        /// 字符转矩阵
        /// </summary>
        /// <param name="matrString"></param>
        /// <returns></returns>
        private Matrix4 StringToMatrx4(string[] matrString)
        {
            double[,] temp = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                string[] ch = { "," };
                string[] str = matrString[i].Split(ch, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < 4; j++)
                {
                    temp[i, j] = Convert.ToDouble(str[j]);
                }
            }
            return new Matrix4(temp);
        }

        /// <summary>
        /// 修改矩阵
        /// </summary>
        /// <param name="matr"></param>
        public void AlterMatr(Matrix4 matr)
        {
            this.WorkMatr = matr;
            AttributeUtils.AttributeOperation("Matrx4", Matrx4ToString(matr), this.PartTag);
        }
        /// <summary>
        /// 升序
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(WorkModel other)
        {
            return this.WorkNumber.CompareTo(other.WorkNumber);
        }
    }
}
