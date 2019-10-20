using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Lab_1_chm
{
    public partial class Form1 : Form
    {
        public int colortype = 0;
        public Form1()
        {
            InitializeComponent();
            zedGraphControl1.GraphPane.XAxis.Title = "Ось X";
            zedGraphControl1.GraphPane.YAxis.Title = "Ось V";
            zedGraphControl1.GraphPane.Title = "График зависимости V(X)";
            zedGraphControl2.GraphPane.XAxis.Title = "Ось X";
            zedGraphControl2.GraphPane.YAxis.Title = "Ось V";
            zedGraphControl2.GraphPane.Title = "График зависимости V(X)";
            zedGraphControl3.GraphPane.XAxis.Title = "Ось X";
            zedGraphControl3.GraphPane.YAxis.Title = "Ось V";
            zedGraphControl3.GraphPane.Title = "График зависимости V(X)";
            zedGraphControl4.GraphPane.XAxis.Title = "Ось X";
            zedGraphControl4.GraphPane.YAxis.Title = "Ось V'";
            zedGraphControl4.GraphPane.Title = "График зависимости V'(X)";
            zedGraphControl5.GraphPane.XAxis.Title = "Ось V";
            zedGraphControl5.GraphPane.YAxis.Title = "Ось V'";
            zedGraphControl5.GraphPane.Title = "График зависимости V'(V)";
        }

        private double func1(double x, double u)
        {
            return -5 / 2 * u;
        }

        private double func2(double x, double u, double h)
        {
            double k1 = func1(x, u);
            double k2 = func1(x + h / 2, u + h / 2 * k1);
            double k3 = func1(x + h / 2, u + h / 2 * k2);
            double k4 = func1(x + h, u + h * k3);
            return u + h / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
        }

        private void DrawGraph(ZedGraphControl pane, ref double[] x, ref double[] v, int colortype)
        {
            GraphPane Pane = pane.GraphPane;
            PointPairList list = new PointPairList();
            for (int i = 0; i < x.Length; i++)
            {
                list.Add(x[i], v[i]);
            }
            if (colortype == 0)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Red, SymbolType.None);
            }
            if (colortype == 1)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Blue, SymbolType.None);
            }
            if (colortype == 2)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Black, SymbolType.None);
            }
            if (colortype == 3)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Purple, SymbolType.None);
            }
            if (colortype == 4)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Green, SymbolType.None);
            }
            if (colortype == 5)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Gold, SymbolType.None);
            }
            if (colortype == 6)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Violet, SymbolType.None);
            }
            if (colortype == 7)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Brown, SymbolType.None);
            }
            pane.AxisChange();
            pane.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            double Step_now = Convert.ToDouble(textBox3.Text);
            int Number_steps = Convert.ToInt32(textBox4.Text);
            double[] x = new double[1];
            double[] v = new double[1];
            double[] u = new double[1];
            x[0] = Convert.ToDouble(textBox1.Text);
            v[0] = Convert.ToDouble(textBox2.Text);
            u[0] = v[0];
            double x_max = Convert.ToDouble(textBox5.Text);
            double Right_wall = Convert.ToDouble(textBox6.Text);
            double c = u[0] / Math.Exp(-5 / 2 * x[0]);
            double[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            result[5] = Step_now;
            result[6] = x[0];
            result[7] = Step_now;
            result[8] = x[0];

            if (checkBox1.Checked == false)
            {
                for (int i = 0; i < Number_steps; i++)
                {
                    Array.Resize<double>(ref x, x.Length + 1);
                    Array.Resize<double>(ref v, v.Length + 1);
                    Array.Resize<double>(ref u, u.Length + 1);
                    x[i + 1] = x[i] + Step_now;
                    if (x[i + 1] > x_max + Right_wall)
                    {
                        result[1] = x_max + Right_wall - x[i];
                        break;
                    }
                    v[i + 1] = func2(x[i], v[i], Step_now);
                    u[i + 1] = c * Math.Exp(-5 / 2 * x[i + 1]);
                    double v_2 = func2(x[i] + Step_now / 2, func2(x[i], v[i], Step_now / 2), Step_now / 2);
                    result[0] = i + 1;
                    double g_e = u[i + 1] - v[i + 1];
                    if (g_e < 0) g_e = -g_e;
                    if (g_e > result[9])
                    {
                        result[9] = g_e;
                        result[10] = x[i + 1];
                    }
                    double l_e = v[i + 1] - v_2;
                    if (l_e < 0) l_e = -l_e;
                    if (l_e / 15 > result[2]) result[2] = l_e / 15;
                    dataGridView1.Rows.Add(i + 1, x[i + 1], v[i + 1], v_2, l_e, l_e / 15, Step_now, '-', '-', u[i + 1], g_e);
                }
                DrawGraph(zedGraphControl1, ref x, ref u, 0);
                DrawGraph(zedGraphControl1, ref x, ref v, colortype);
                colortype++;
                if (colortype == 8)
                {
                    colortype = 0;
                }
            }
            else
            {
                double p_l_e = Convert.ToDouble(textBox7.Text);
                double del = 0;
                double dou = 0;
                for (int i = 0; i < Number_steps; i++)
                {
                    if (x[i] + Step_now > x_max + Right_wall)
                    {
                        result[1] = x_max + Right_wall - x[i];
                        result[3] = dou;
                        result[4] = del;
                        break;
                    }
                    Array.Resize<double>(ref v, v.Length + 1);
                    Array.Resize<double>(ref u, u.Length + 1);
                    v[i + 1] = func2(x[i], v[i], Step_now);
                    u[i + 1] = c * Math.Exp(-5 / 2 * (x[i] + Step_now));
                    double v_2 = func2(x[i] + Step_now / 2, func2(x[i], v[i], Step_now / 2), Step_now / 2);
                    result[0] = i + 1;
                    double g_e = u[i + 1] - v[i + 1];
                    double l_e = v[i + 1] - v_2;
                    if (l_e < 0) l_e = -l_e;
                    if (l_e / 15 > p_l_e)
                    {
                        Array.Resize<double>(ref v, v.Length - 1);
                        Array.Resize<double>(ref u, u.Length - 1);
                        Step_now = Step_now / 2;
                        del++;
                        i--;
                    }
                    if (l_e / 15 <= p_l_e && l_e / 15 > p_l_e / 32)
                    {
                        Array.Resize<double>(ref x, x.Length + 1);
                        x[i + 1] = x[i] + Step_now;
                        dataGridView1.Rows.Add(i + 1, x[i + 1], v[i + 1], v_2, l_e, l_e / 15, Step_now, dou, del, u[i + 1], g_e);
                        if (Step_now > result[5])
                        {
                            result[5] = Step_now;
                            result[6] = x[i + 1];
                        }
                        if (Step_now < result[7])
                        {
                            result[7] = Step_now;
                            result[8] = x[i + 1];
                        }
                        if (g_e < 0) g_e = -g_e;
                        if (g_e > result[9])
                        {
                            result[9] = g_e;
                            result[10] = x[i + 1];
                        }
                        if (l_e / 15 > result[2]) result[2] = l_e / 15;
                    }
                    if (l_e / 15 <= p_l_e / 32)
                    {
                        Array.Resize<double>(ref x, x.Length + 1);
                        x[i + 1] = x[i] + Step_now;
                        Step_now = Step_now * 2;
                        dataGridView1.Rows.Add(i + 1, x[i + 1], v[i + 1], v_2, l_e, l_e / 15, Step_now, dou, del, u[i + 1], g_e);
                        dou++;
                        if (Step_now > result[5])
                        {
                            result[5] = Step_now;
                            result[6] = x[i + 1];
                        }
                        if (Step_now < result[7])
                        {
                            result[7] = Step_now;
                            result[8] = x[i + 1];
                        }
                        if (g_e < 0) g_e = -g_e;
                        if (g_e > result[9])
                        {
                            result[9] = g_e;
                            result[10] = x[i + 1];
                        }
                        if (l_e / 15 > result[2]) result[2] = l_e / 15;
                    }                                       
                }
                DrawGraph(zedGraphControl1, ref x, ref u, 0);
                DrawGraph(zedGraphControl1, ref x, ref v, colortype);
                colortype++;
                if (colortype == 8)
                {
                    colortype = 0;
                }
            }
            label28.Text = Convert.ToString(result[0]);
            label29.Text = Convert.ToString(result[1]);
            label30.Text = Convert.ToString(result[2]);
            label31.Text = Convert.ToString(result[3]);
            label32.Text = Convert.ToString(result[4]);
            label33.Text = Convert.ToString(result[5]);
            label34.Text = Convert.ToString(result[6]);
            label35.Text = Convert.ToString(result[7]);
            label36.Text = Convert.ToString(result[8]);
            label37.Text = Convert.ToString(result[9]);
            label38.Text = Convert.ToString(result[10]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            dataGridView1.Rows.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.XAxis.Min = Convert.ToDouble(textBox8.Text);
            zedGraphControl1.GraphPane.XAxis.Max = Convert.ToDouble(textBox9.Text);
            zedGraphControl1.GraphPane.YAxis.Min = Convert.ToDouble(textBox10.Text);
            zedGraphControl1.GraphPane.YAxis.Max = Convert.ToDouble(textBox11.Text);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.XAxis.MinAuto = true;
            zedGraphControl1.GraphPane.YAxis.MinAuto = true;
            zedGraphControl1.GraphPane.XAxis.MaxAuto = true;
            zedGraphControl1.GraphPane.YAxis.MaxAuto = true;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private double func3(double x, double v)
        {
            return Math.Log(1 + x) / (1 + x * x) * v * v + v - v * v * v * Math.Sin(10 * x);;
        }

        private double func4(double x, double u, double h)
        {
            double k1 = func3(x, u);
            double k2 = func3(x + h / 2, u + h / 2 * k1);
            double k3 = func3(x + h / 2, u + h / 2 * k2);
            double k4 = func3(x + h, u + h * k3);
            return u + h / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            double Step_now = Convert.ToDouble(textBox14.Text);
            int Number_steps = Convert.ToInt32(textBox15.Text);
            double[] x = new double[1];
            double[] v = new double[1];
            x[0] = Convert.ToDouble(textBox12.Text);
            v[0] = Convert.ToDouble(textBox13.Text);
            double x_max = Convert.ToDouble(textBox16.Text);
            double Right_wall = Convert.ToDouble(textBox17.Text);
            double[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            result[5] = Step_now;
            result[6] = x[0];
            result[7] = Step_now;
            result[8] = x[0];

            if (checkBox2.Checked == false)
            {
                for (int i = 0; i < Number_steps; i++)
                {
                    Array.Resize<double>(ref x, x.Length + 1);
                    Array.Resize<double>(ref v, v.Length + 1);
                    x[i + 1] = x[i] + Step_now;
                    if (x[i + 1] > x_max + Right_wall)
                    {
                        result[1] = x_max + Right_wall - x[i];
                        break;
                    }
                    v[i + 1] = func4(x[i], v[i], Step_now);
                    double v_2 = func4(x[i] + Step_now / 2, func4(x[i], v[i], Step_now / 2), Step_now / 2);
                    result[0] = i + 1;
                    double l_e = v[i + 1] - v_2;
                    if (l_e < 0) l_e = -l_e;
                    if (l_e / 15 > result[2]) result[2] = l_e / 15;
                    dataGridView2.Rows.Add(i + 1, x[i + 1], v[i + 1], v_2, l_e, l_e / 15, Step_now, '-', '-');
                }
                DrawGraph(zedGraphControl2, ref x, ref v, colortype);
                colortype++;
                if (colortype == 8)
                {
                    colortype = 0;
                }
            }
            else
            {
                double p_l_e = Convert.ToDouble(textBox7.Text);
                double del = 0;
                double dou = 0;
                for (int i = 0; i < Number_steps; i++)
                {
                    if (x[i] + Step_now > x_max + Right_wall)
                    {
                        result[1] = x_max + Right_wall - x[i];
                        result[3] = dou;
                        result[4] = del;
                        break;
                    }
                    Array.Resize<double>(ref v, v.Length + 1);
                    v[i + 1] = func4(x[i], v[i], Step_now);
                    double v_2 = func4(x[i] + Step_now / 2, func4(x[i], v[i], Step_now / 2), Step_now / 2);
                    result[0] = i + 1;
                    double l_e = v[i + 1] - v_2;
                    if (l_e < 0) l_e = -l_e;
                    if (l_e / 15 > p_l_e)
                    {
                        Array.Resize<double>(ref v, v.Length - 1);
                        Step_now = Step_now / 2;
                        del++;
                        i--;
                    }
                    if (l_e / 15 <= p_l_e && l_e / 15 > p_l_e / 32)
                    {
                        Array.Resize<double>(ref x, x.Length + 1);
                        x[i + 1] = x[i] + Step_now;
                        dataGridView2.Rows.Add(i + 1, x[i + 1], v[i + 1], v_2, l_e, l_e / 15, Step_now, dou, del);
                        if (Step_now > result[5])
                        {
                            result[5] = Step_now;
                            result[6] = x[i + 1];
                        }
                        if (Step_now < result[7])
                        {
                            result[7] = Step_now;
                            result[8] = x[i + 1];
                        }
                        if (l_e / 15 > result[2]) result[2] = l_e / 15;
                    }
                    if (l_e / 15 <= p_l_e / 32)
                    {
                        Array.Resize<double>(ref x, x.Length + 1);
                        x[i + 1] = x[i] + Step_now;
                        Step_now = Step_now * 2;
                        dataGridView2.Rows.Add(i + 1, x[i + 1], v[i + 1], v_2, l_e, l_e / 15, Step_now, dou, del);
                        dou++;
                        if (Step_now > result[5])
                        {
                            result[5] = Step_now;
                            result[6] = x[i + 1];
                        }
                        if (Step_now < result[7])
                        {
                            result[7] = Step_now;
                            result[8] = x[i + 1];
                        }
                        if (l_e / 15 > result[2]) result[2] = l_e / 15;
                    }
                }
                DrawGraph(zedGraphControl2, ref x, ref v, colortype);
                colortype++;
                if (colortype == 8)
                {
                    colortype = 0;
                }
            }
            label64.Text = Convert.ToString(result[0]);
            label65.Text = Convert.ToString(result[1]);
            label66.Text = Convert.ToString(result[2]);
            label67.Text = Convert.ToString(result[3]);
            label68.Text = Convert.ToString(result[4]);
            label69.Text = Convert.ToString(result[5]);
            label70.Text = Convert.ToString(result[6]);
            label71.Text = Convert.ToString(result[7]);
            label72.Text = Convert.ToString(result[8]);
        }

        private double func5(double x, double v, double v_, double a, double b)
        {
            return -a * v_ * v_ - b * Math.Sin(v);
        }
      
        private double func6(double x, double v, double v_, double a, double b)
        {
            return v_;
        }

        private double[] func7(double x, double v, double v_, double a, double b, double h)
        {
            double k1 = func5(x, v, v_, a, b);
            double l1 = func6(x, v, v_, a, b);
            double k2 = func5(x + h / 2, v + h / 2 * k1, v_ + h / 2 * l1, a, b);
            double l2 = func6(x + h / 2, v + h / 2 * k1, v_ + h / 2 * l1, a, b);
            double k3 = func5(x + h / 2, v + h / 2 * k2, v_ + h / 2 * l2, a, b);
            double l3 = func6(x + h / 2, v + h / 2 * k2, v_ + h / 2 * l2, a, b);
            double k4 = func5(x + h, v + h * k3, v_ + h * l3, a, b);
            double l4 = func6(x + h, v + h * k3, v_ + h * l3, a, b);
            double[] result = { v + h / 6 * (k1 + 2 * k2 + 2 * k3 + k4), v_ + h / 6 * (l1 + 2 * l2 + 2 * l3 + l4) };
            return result;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            double Step_now = Convert.ToDouble(textBox26.Text);
            int Number_steps = Convert.ToInt32(textBox27.Text);
            double[] x = new double[1];
            double[] v = new double[1];
            double[] v_ = new double[1];
            x[0] = Convert.ToDouble(textBox23.Text);
            v[0] = Convert.ToDouble(textBox24.Text);
            v_[0] = Convert.ToDouble(textBox25.Text);
            double x_max = Convert.ToDouble(textBox28.Text);
            double Right_wall = Convert.ToDouble(textBox29.Text);
            double a = Convert.ToDouble(textBox35.Text);
            double b = Convert.ToDouble(textBox36.Text);
            double[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            result[5] = Step_now;
            result[6] = x[0];
            result[7] = Step_now;
            result[8] = x[0];

            if (checkBox3.Checked == false)
            {
                for (int i = 0; i < Number_steps; i++)
                {
                    Array.Resize<double>(ref x, x.Length + 1);
                    Array.Resize<double>(ref v, v.Length + 1);
                    Array.Resize<double>(ref v_, v_.Length + 1);
                    x[i + 1] = x[i] + Step_now;
                    if (x[i + 1] > x_max + Right_wall)
                    {
                        result[1] = x_max + Right_wall - x[i];
                        break;
                    }
                    double[] v_v = { 0, 0 };
                    v_v = func7(x[i], v[i], v_[i], a, b, Step_now);
                    v[i + 1] = v_v[0];
                    v_[i + 1] = v_v[1];
                    v_v = func7(x[i], v[i], v_[i], a, b, Step_now / 2);
                    v_v = func7(x[i] + Step_now / 2, v_v[0], v_v[1], a, b, Step_now / 2);
                    result[0] = i + 1;
                    double l_e_1 = v[i + 1] - v_v[0];
                    double l_e_2 = v_[i + 1] - v_v[0];
                    if (l_e_1 < 0) l_e_1 = -l_e_1;
                    if (l_e_2 < 0) l_e_2 = -l_e_2;
                    double l_e = Math.Max(l_e_1, l_e_2);
                    if (l_e / 15 > result[2]) result[2] = l_e / 15;
                    dataGridView3.Rows.Add(i + 1, x[i + 1], v[i + 1], v_v[0], v_v[0] - v[i + 1], v_[i + 1], v_v[1], v_v[1] - v_[i + 1], l_e / 15, Step_now, '-', '-');
                }
                DrawGraph(zedGraphControl3, ref x, ref v, colortype);
                DrawGraph(zedGraphControl4, ref x, ref v_, colortype);
                DrawGraph(zedGraphControl5, ref v, ref v_, colortype);
                colortype++;
                if (colortype == 8)
                {
                    colortype = 0;
                }
            }
            else
            {
                double p_l_e = Convert.ToDouble(textBox30.Text);
                double del = 0;
                double dou = 0;
                for (int i = 0; i < Number_steps; i++)
                {
                    if (x[i] + Step_now > x_max + Right_wall)
                    {
                        result[1] = x_max + Right_wall - x[i];
                        result[3] = dou;
                        result[4] = del;
                        break;
                    }
                    Array.Resize<double>(ref v, v.Length + 1);
                    Array.Resize<double>(ref v_, v_.Length + 1);
                    double[] v_v = { 0, 0 };
                    v_v = func7(x[i], v[i], v_[i], a, b, Step_now);
                    v[i + 1] = v_v[0];
                    v_[i + 1] = v_v[1];
                    v_v = func7(x[i], v[i], v_[i], a, b, Step_now / 2);
                    v_v = func7(x[i] + Step_now / 2, v_v[0], v_v[1], a, b, Step_now / 2);
                    result[0] = i + 1;
                    double l_e_1 = v[i + 1] - v_v[0];
                    double l_e_2 = v_[i + 1] - v_v[1];
                    if (l_e_1 < 0) l_e_1 = -l_e_1;
                    if (l_e_2 < 0) l_e_2 = -l_e_2;
                    double l_e = Math.Max(l_e_1, l_e_2);
                    if (l_e / 15 > p_l_e)
                    {
                        Array.Resize<double>(ref v, v.Length - 1);
                        Array.Resize<double>(ref v_, v_.Length - 1);
                        Step_now = Step_now / 2;
                        del++;
                        i--;
                    }
                    if (l_e / 15 <= p_l_e && l_e / 15 > p_l_e / 32)
                    {
                        Array.Resize<double>(ref x, x.Length + 1);
                        x[i + 1] = x[i] + Step_now;
                        dataGridView3.Rows.Add(i + 1, x[i + 1], v[i + 1], v_v[0], v_v[0] - v[i + 1], v_[i + 1], v_v[1], v_v[1] - v_[i + 1], l_e / 15, Step_now, dou, del);
                        if (Step_now > result[5])
                        {
                            result[5] = Step_now;
                            result[6] = x[i + 1];
                        }
                        if (Step_now < result[7])
                        {
                            result[7] = Step_now;
                            result[8] = x[i + 1];
                        }
                        if (l_e / 15 > result[2]) result[2] = l_e / 15;
                    }
                    if (l_e / 15 <= p_l_e / 32)
                    {
                        Array.Resize<double>(ref x, x.Length + 1);
                        x[i + 1] = x[i] + Step_now;
                        Step_now = Step_now * 2;
                        dataGridView3.Rows.Add(i + 1, x[i + 1], v[i + 1], v_v[0], v_v[0] - v[i + 1], v_[i + 1], v_v[1], v_v[1] - v_[i + 1], l_e / 15, Step_now, dou, del);
                        dou++;
                        if (Step_now > result[5])
                        {
                            result[5] = Step_now;
                            result[6] = x[i + 1];
                        }
                        if (Step_now < result[7])
                        {
                            result[7] = Step_now;
                            result[8] = x[i + 1];
                        }
                        if (l_e / 15 > result[2]) result[2] = l_e / 15;
                    }
                }
                DrawGraph(zedGraphControl3, ref x, ref v, colortype);
                DrawGraph(zedGraphControl4, ref x, ref v_, colortype);
                DrawGraph(zedGraphControl5, ref v, ref v_, colortype);
                colortype++;
                if (colortype == 8)
                {
                    colortype = 0;
                }
            }
            label99.Text = Convert.ToString(result[0]);
            label100.Text = Convert.ToString(result[1]);
            label101.Text = Convert.ToString(result[2]);
            label102.Text = Convert.ToString(result[3]);
            label103.Text = Convert.ToString(result[4]);
            label104.Text = Convert.ToString(result[5]);
            label105.Text = Convert.ToString(result[6]);
            label106.Text = Convert.ToString(result[7]);
            label107.Text = Convert.ToString(result[8]);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            zedGraphControl2.GraphPane.CurveList.Clear();
            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
            dataGridView2.Rows.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            zedGraphControl2.GraphPane.XAxis.Min = Convert.ToDouble(textBox19.Text);
            zedGraphControl2.GraphPane.XAxis.Max = Convert.ToDouble(textBox20.Text);
            zedGraphControl2.GraphPane.YAxis.Min = Convert.ToDouble(textBox21.Text);
            zedGraphControl2.GraphPane.YAxis.Max = Convert.ToDouble(textBox22.Text);
            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.XAxis.MinAuto = true;
            zedGraphControl1.GraphPane.YAxis.MinAuto = true;
            zedGraphControl1.GraphPane.XAxis.MaxAuto = true;
            zedGraphControl1.GraphPane.YAxis.MaxAuto = true;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            zedGraphControl3.GraphPane.CurveList.Clear();
            zedGraphControl3.AxisChange();
            zedGraphControl3.Invalidate();
            zedGraphControl4.GraphPane.CurveList.Clear();
            zedGraphControl4.AxisChange();
            zedGraphControl4.Invalidate();
            zedGraphControl5.GraphPane.CurveList.Clear();
            zedGraphControl5.AxisChange();
            zedGraphControl5.Invalidate();
            dataGridView3.Rows.Clear();
        }
    }
}
