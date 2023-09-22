using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    internal class BlockPanel : BasicBlockPanel
    {
        public string Header
        {
            get { return HeadText; }
            set { HeadText = value; }
        }

        public BlockPanel()
        {

        }
    }
}
