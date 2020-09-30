using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kursy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursesTabbedPage : TabbedPage
    {
        public CoursesTabbedPage()
        {
            InitializeComponent();
        }
    }
}
