﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;

namespace MolexPlugin
{
    public class test
    {

        public static void ces()
        {
            UFSession theUFSession = UFSession.GetUFSession();
            //Body body1 = NXObjectManager.Get((Tag)51633) as Body;
            //Body body2 = NXObjectManager.Get((Tag)51611) as Body;
            //AnalysisUtils.SetInterference(body1, body2);
            Tag face1 = (Tag)82809;
            Tag face2 = (Tag)82803;
            Tag face3;
            theUFSession.Modl.IntersectBodiesWithRetainedOptions(face1, face2, false, false, out face3);
        }


    }
}
