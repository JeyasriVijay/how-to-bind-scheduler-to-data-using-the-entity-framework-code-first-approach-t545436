﻿using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Bars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DXSample.Behaviors {
    public class BarStaticItemResetBehavior : Behavior<BarStaticItem> {
        DispatcherTimer timer;
        public string Value {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(BarStaticItemResetBehavior), new PropertyMetadata(string.Empty, OnChanged));
        protected override void OnAttached() {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            base.OnAttached();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            AssociatedObject.SetValue(BarStaticItem.ContentProperty, null);
            timer.Stop();
        }
        protected override void OnDetaching() {
            timer.Stop();
            base.OnDetaching();
        }
        static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BarStaticItemResetBehavior barStaticItemResetBehavior = (d as BarStaticItemResetBehavior);
            if (barStaticItemResetBehavior.IsAttached) {
                barStaticItemResetBehavior.AssociatedObject.SetValue(BarStaticItem.ContentProperty, e.NewValue);
                barStaticItemResetBehavior.timer.Start();
            }
        }
    }
}
