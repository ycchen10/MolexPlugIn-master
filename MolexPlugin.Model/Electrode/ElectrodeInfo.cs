﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NXOpen;
using Basic;

namespace MolexPlugin.Model
{
    public class ElectrodeInfo
    {  
        /// <summary>
        /// 电极名
        /// </summary>
        public string EleName { get; set; }
        /// <summary>
        /// X向PH
        /// </summary>
        public double PitchX { get; set; } = 0;
        /// <summary>
        /// X向个数
        /// </summary>
        public int PitchXNum { get; set; } = 0;
        /// <summary>
        /// Y向PH
        /// </summary>
        public double PitchY { get; set; } = 0;
        /// <summary>
        /// Y向个数
        /// </summary>
        public int PitchYNum { get; set; } = 0;
        /// <summary>
        /// 粗放
        /// </summary>
        public double CrudeInter { get; set; } = 0;
        /// <summary>
        /// 粗放个数
        /// </summary>
        public int CrudeNum { get; set; } = 0;
        /// <summary>
        /// 中放
        /// </summary>
        public double DuringInter { get; set; } = 0;
        /// <summary>
        /// 中放个数
        /// </summary>
        public int DuringNum { get; set; } = 0;
        /// <summary>
        /// 精放
        /// </summary>
        public double FineInter { get; set; } = 0;
        /// <summary>
        /// 精放个数
        /// </summary>
        public int FineNum { get; set; } = 0;
        /// <summary>
        /// 材料
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// 电极类型
        /// </summary>
        public string EleType { get; set; }
        /// <summary>
        /// 放电条件
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 拉升值
        /// </summary>
        public double Extrudewith { get; set; }
        /// <summary>
        /// CH值
        /// </summary>
        public string Ch { get; set; }
        /// <summary>
        /// 借用电极
        /// </summary>
        public string BorrowName { get; set; }
        /// <summary>
        /// 电极设定参数
        /// </summary>
        public double[] EleSetValue { get; set; } = new double[3];
        /// <summary>
        /// 备料值
        /// </summary>
        public int[] Preparation { get; set; } = new int[3];
        /// <summary>
        /// 标准料
        /// </summary>
        public bool IsPreparation { get; set; }
        /// <summary>
        /// 电极描述
        /// </summary>
        public string ElePresentation { get; set; }
        ///// <summary>
        ///// 旋转电极
        ///// </summary>
        //public bool Rotate { get; set; }
        ///// <summary>
        ///// 清角电极
        ///// </summary>
        //public bool ClearEngle { get; set; }
        ///// <summary>
        ///// 线割电极
        ///// </summary>
        //public bool Wedm { get; set; }
        ///// <summary>
        ///// 去毛刺电极
        ///// </summary>
        //public bool ClearGlitch { get; set; }
        ///// <summary>
        ///// 交口电极
        ///// </summary>
        //public bool Gate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 工艺
        /// </summary>
        public string Technology { get; set; }
        /// <summary>
        /// 加工模板
        /// </summary>
        public string CamTemplate { get; set; }
        /// <summary>
        /// 放电面积
        /// </summary>
        public double Area { get; set; }
        /// <summary>
        /// 电极编号
        /// </summary>
        public int EleNumber { get; set; }
        /// <summary>
        /// 跑位
        /// </summary>
        public string Positioning { get; set; }
        /// <summary>
        /// /Z向基准
        /// </summary>
        public bool ZDatum { get; set; }
        /// <summary>
        /// 设置属性
        /// </summary>
        public void SetAttribute(Part obj)
        {   
            AttributeUtils.AttributeOperation("EleName", this.EleName, obj);
            AttributeUtils.AttributeOperation("BorrowName", this.BorrowName, obj);

            AttributeUtils.AttributeOperation("PitchX", this.PitchX, obj);
            AttributeUtils.AttributeOperation("PitchXNum", this.PitchXNum, obj);
            AttributeUtils.AttributeOperation("PitchY", this.PitchY, obj);
            AttributeUtils.AttributeOperation("PitchYNum", this.PitchYNum, obj);

            AttributeUtils.AttributeOperation("CrudeInter", this.CrudeInter, obj);
            AttributeUtils.AttributeOperation("CrudeNum", this.CrudeNum, obj);
            AttributeUtils.AttributeOperation("DuringInter", this.DuringInter, obj);
            AttributeUtils.AttributeOperation("DuringNum", this.DuringNum, obj);
            AttributeUtils.AttributeOperation("FineInter", this.FineInter, obj);
            AttributeUtils.AttributeOperation("FineNum", this.FineNum, obj);

            AttributeUtils.AttributeOperation("Material1", this.Material, obj);
            AttributeUtils.AttributeOperation("EleType", this.EleType, obj);
            AttributeUtils.AttributeOperation("Condition", this.Condition, obj);
            AttributeUtils.AttributeOperation("Extrudewith", this.Extrudewith, obj);
            AttributeUtils.AttributeOperation("CH", this.Ch, obj);

            AttributeUtils.AttributeOperation("IsPreparation", this.IsPreparation, obj);
            AttributeUtils.AttributeOperation("Remarks", this.Remarks, obj);
            AttributeUtils.AttributeOperation("Technology", this.Technology, obj);
            AttributeUtils.AttributeOperation("CamTemplate", this.CamTemplate, obj);

            AttributeUtils.AttributeOperation("EleSetValue", this.EleSetValue, obj);
            AttributeUtils.AttributeOperation("Preparation", this.Preparation, obj);
            AttributeUtils.AttributeOperation("ElePresentation", this.ElePresentation, obj);
            AttributeUtils.AttributeOperation("Area", this.Area, obj);
            AttributeUtils.AttributeOperation("EleNumber", this.EleNumber, obj);
            AttributeUtils.AttributeOperation("Positioning", this.Positioning, obj);
        }
        /// <summary>
        /// 读取属性
        /// </summary>
        public void GetAttribute(Part obj)
        {          
            this.EleName = AttributeUtils.GetAttrForString(obj, "EleName");
            this.BorrowName = AttributeUtils.GetAttrForString(obj, "BorrowName");

            this.PitchX = AttributeUtils.GetAttrForDouble(obj, "PitchX");
            this.PitchXNum = AttributeUtils.GetAttrForInt(obj, "PitchXNum");
            this.PitchY = AttributeUtils.GetAttrForDouble(obj, "PitchY");
            this.PitchYNum = AttributeUtils.GetAttrForInt(obj, "PitchYNum");

            this.CrudeInter = AttributeUtils.GetAttrForDouble(obj, "CrudeInter");
            this.CrudeNum = AttributeUtils.GetAttrForInt(obj, "CrudeNum");
            this.DuringInter = AttributeUtils.GetAttrForDouble(obj, "DuringInter");
            this.DuringNum = AttributeUtils.GetAttrForInt(obj, "DuringNum");
            this.FineInter = AttributeUtils.GetAttrForDouble(obj, "FineInter");
            this.FineNum = AttributeUtils.GetAttrForInt(obj, "FineNum");

            this.Material = AttributeUtils.GetAttrForString(obj, "Material1");
            this.EleType = AttributeUtils.GetAttrForString(obj, "EleType");
            this.Condition = AttributeUtils.GetAttrForString(obj, "Condition");
            this.Extrudewith = AttributeUtils.GetAttrForDouble(obj, "Extrudewith");
            this.Ch = AttributeUtils.GetAttrForString(obj, "CH");

            this.IsPreparation = AttributeUtils.GetAttrForBool(obj, "IsPreparation");
            this.Remarks = AttributeUtils.GetAttrForString(obj, "Remarks");
            this.Technology = AttributeUtils.GetAttrForString(obj, "Technology");
            this.CamTemplate = AttributeUtils.GetAttrForString(obj, "CamTemplate");

            for (int i = 0; i < 3; i++)
            {
                this.EleSetValue[i] = AttributeUtils.GetAttrForDouble(obj, "EleSetValue", i);
            }
            for (int i = 0; i < 3; i++)
            {
                this.Preparation[i] = AttributeUtils.GetAttrForInt(obj, "Preparation", i);
            }

            this.Positioning = AttributeUtils.GetAttrForString(obj, "Positioning");
            this.ElePresentation = AttributeUtils.GetAttrForString(obj, "ElePresentation");
            this.Area = AttributeUtils.GetAttrForDouble(obj, "Area");
            this.EleNumber = AttributeUtils.GetAttrForInt(obj, "EleNumber");
        }

        
    }
}
