using Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Review
{
    public partial class ReviewList : UserBaseForm
    {
        public ReviewSetDto ReviewSetDto = new ReviewSetDto();
        private readonly CrisisAppService crisisAppService = new CrisisAppService();
       
        public ReviewList()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ReviewEdit reviewEdit = new ReviewEdit();        
            if (reviewEdit.ShowDialog() == DialogResult.OK)
            {
                showList();
            }
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            var review = gridViewReview.GetFocusedRow() as ReviewSetDto;
            if (review == null)
            {
                ShowMessageBoxInformation("尚未选定数据");
                return;
            }
            ReviewEdit reviewEdit = new ReviewEdit(review);            
            if (reviewEdit.ShowDialog() == DialogResult.OK)
            {
                showList();
            }
        }

        private void ReviewList_Load(object sender, EventArgs e)
        {
            showList();
        }
        private void showList()
        {
            var recvir = crisisAppService.getAllReview();
            gridControl1.DataSource = recvir;

        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            butEdit.PerformClick();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            var review = gridViewReview.GetFocusedRow() as ReviewSetDto;
            if (review == null)
            {
                ShowMessageBoxInformation("尚未选定数据");
                return;
            }
            ReviewSetDto.Id = review.Id;
            crisisAppService.DelReviewSet(ReviewSetDto);
            showList();
        }
    }
}
