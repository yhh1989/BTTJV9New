using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace HealthExaminationSystem.Win.ErrorInfo
{
    public partial class Error : UserBaseForm
    {
        private Exception E { get; }

        public Error(Exception e)
        {
            InitializeComponent();

            E = e;
            gridViewClassModel.DataController.AllowIEnumerableDetails = true;
        }

        private void Error_Load(object sender, EventArgs e)
        {
            if (E != null)
            {
                var result = ForeachClassProperties(E);
                gridControl.DataSource = result;
            }
        }

        /// <summary>
        /// C#反射遍历对象属性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">对象</param>
        /// <param name="i"></param>
        public List<ClassModel> ForeachClassProperties<T>(T model, int i = 0)
        {
            var t = model.GetType();
            var models = new List<ClassModel>();
            var propertyList = t.GetProperties();
            foreach (var item in propertyList)
            {
                try
                {
                    var value = new ClassModel
                    {
                        Name = item.Name,
                        Description = item.GetValue(model, null),
                        Type = item.PropertyType.Name
                    };
                    if (item.PropertyType.IsClass && item.PropertyType != typeof(string))
                    {
                        if (i != 5)
                        {
                            i = i + 1;
                            value.Models = ForeachClassProperties(item.GetValue(model, null), i);
                        }

                    }
                    models.Add(value);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return models;
        }

    }
}