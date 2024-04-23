using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
	public partial class InspectionTotalPicture : UserBaseForm
	{
		private PictureArg _pictureArg;
        public System.Drawing.Point mouseDownPoint;//存储鼠标焦点的全局变量
        public bool isSelected = false;
        //项目图像数
        int CustomerPicSys = 0;
        public InspectionTotalPicture(PictureArg pictureArg)
		{
			_pictureArg = pictureArg;
			InitializeComponent();
		}

		private void InspectionTotalPicture_Load(object sender, EventArgs e)
		{
			var itemPicture = _pictureArg?.Pictures.Where(r => r.ItemBMID == _pictureArg.CurrentItemId).ToList();
			if (itemPicture.Count>0 && itemPicture.FirstOrDefault()?.PictureBM != null)
			{
				var picture =
					DefinedCacheHelper.DefinedApiProxy.PictureController.GetUrl(itemPicture.FirstOrDefault().PictureBM.Value);
				if (picture != null)
				{
					pictureEditTuPianZhanShi.LoadAsync(picture.RelativePath);
				}
			}
            labelControlZongShu.Text = itemPicture.Count.ToString();
            if (itemPicture.Count > 0)
            {
                labelControlDangQian.Text = "1";
            }
            else
            { labelControlDangQian.Text = "0"; }
        }

		private void pictureEdit1_Click(object sender, EventArgs e)
		{
			//var index = _pictureArg.Pictures.FindIndex(r => r.ItemBMID == _pictureArg.CurrentItemId);
            if (CustomerPicSys == 0)
                CustomerPicSys = _pictureArg.Pictures.Count - 1;
            else
                CustomerPicSys = CustomerPicSys - 1;
   //         if (index == 0)
			//{
			//	index = _pictureArg.Pictures.Count - 1;
			//}

			var itemPicture = _pictureArg?.Pictures[CustomerPicSys];
			if (itemPicture.ItemBMID != null)
			{
				_pictureArg.CurrentItemId = itemPicture.ItemBMID.Value;
			}

			if (itemPicture.PictureBM != null)
			{
				AutoLoading(() =>
				{
					var picture =
						DefinedCacheHelper.DefinedApiProxy.PictureController.GetUrl(itemPicture.PictureBM.Value);
					if (picture != null)
					{
						pictureEditTuPianZhanShi.LoadAsync(picture.RelativePath);
					}
				});
			}
            labelControlDangQian.Text = (CustomerPicSys + 1).ToString();
        }

		private void pictureEdit3_Click(object sender, EventArgs e)
		{
			//var index = _pictureArg.Pictures.FindIndex(r => r.ItemBMID == _pictureArg.CurrentItemId);
            if (CustomerPicSys == _pictureArg.Pictures.Count - 1)
                CustomerPicSys = 0;
            else
                CustomerPicSys = CustomerPicSys + 1;
   //         if (index == _pictureArg.Pictures.Count - 1)
			//{
			//	index = 0;
			//}

			var itemPicture = _pictureArg?.Pictures[CustomerPicSys];
			if (itemPicture.ItemBMID != null)
			{
				_pictureArg.CurrentItemId = itemPicture.ItemBMID.Value;
			}
			if (itemPicture.PictureBM != null)
			{
				AutoLoading(() =>
				{
					var picture =
						DefinedCacheHelper.DefinedApiProxy.PictureController.GetUrl(itemPicture.PictureBM.Value);
					if (picture != null)
					{
						pictureEditTuPianZhanShi.LoadAsync(picture.RelativePath);
					}
				});
			}
            labelControlDangQian.Text = (CustomerPicSys + 1).ToString();
        }

        private void pictureEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }
        #region 图片鼠标事件     

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            double scale = 1;
            if (pictureEditTuPianZhanShi.Height > 0)
            {
                scale = (double)pictureEditTuPianZhanShi.Width / (double)pictureEditTuPianZhanShi.Height;
            }
            pictureEditTuPianZhanShi.Width += (int)(e.Delta * scale);
            pictureEditTuPianZhanShi.Height += e.Delta;
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureEditTuPianZhanShi.Focus();
            pictureEditTuPianZhanShi.Cursor = Cursors.SizeAll;
        }

        //在MouseDown处获知鼠标是否按下，并记录下此时的鼠标坐标值；
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;  //注：全局变量mouseDownPoint前面已定义为Point类型  
                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
        }

        //在MouseUp处获知鼠标是否松开，终止拖动操作；
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;

        }

        private bool IsMouseInPanel()
        {
            if (this.panel_Picture.Left < PointToClient(Cursor.Position).X
                    && PointToClient(Cursor.Position).X < this.panel_Picture.Left
                    + this.panel_Picture.Width && this.panel_Picture.Top
                    < PointToClient(Cursor.Position).Y && PointToClient(Cursor.Position).Y
                    < this.panel_Picture.Top + this.panel_Picture.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //图片平移,在MouseMove处添加拖动函数操作
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && IsMouseInPanel())//确定已经激发MouseDown事件，和鼠标在picturebox的范围内
            {
                this.pictureEditTuPianZhanShi.Left = this.pictureEditTuPianZhanShi.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pictureEditTuPianZhanShi.Top = this.pictureEditTuPianZhanShi.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }

        }
        #endregion

        private void panel_Picture_SizeChanged(object sender, EventArgs e)
        {
            pictureEditTuPianZhanShi.Width = panel_Picture.Width - 6;
            pictureEditTuPianZhanShi.Height = panel_Picture.Height - 6;
        }
    }
}
