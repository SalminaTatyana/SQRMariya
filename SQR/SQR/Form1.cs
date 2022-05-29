using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQR
{

    public partial class Form1 : Form
    {
        //Словарь невалидныхх символов
        char[] regex = {'q','w','e','r','t','y','u','i','o','p','[',']','a','s','d','f','g','h','j','k','l',';','z','x','c','v','b','n','m','<','>','/','?',':',
                'й', 'ц', 'у', 'к', 'е', 'н', 'г', 'ш', 'щ', 'з', 'х', 'ъ', 'ф', 'ы', 'в', 'а', 'п', 'р', 'о', 'л', 'д', 'ж', 'э', 'я', 'ч', 'с', 'м', 'и', 'т', 'ь', 'б', 'ю',
                '+', '=','`','ё','!','@','#','$','%','^','&','*','(',')','|'};
        //Инициализация формы (окна) автосоздаваемый код
        public Form1()
        {
            InitializeComponent();
            textBox1.TextChanged += textBox1_TextChanged;
        }
        //Функция проверки вводимых данных в поле х, проверка происходит при каждом изменении поля
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in regex)
            {
                //проверка данных на допустимые символы
                if (textBox1.Text.Contains(item.ToString()))
                {
                    label3.Text = "Ошибка ввода X. Удалите символ " + item.ToString() + " с индексом " + (textBox1.Text.IndexOf(item) + 1);
                    break;
                }
                else label3.Text = "";
            }

        }
        //Функция проверки вводимых данных в поле у, проверка происходит при каждом изменении поля
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in regex)
            {
                //проверка данных на допустимые символы
                if (textBox2.Text.Contains(item.ToString()))
                {
                    label4.Text = "Ошибка ввода Y. Удалите символ " + item.ToString() + " с индексом " + (textBox2.Text.IndexOf(item) + 1);
                    break;
                }
                else label4.Text = "";
            }
        }
        //Функция проверки полей ввода на двойные пробелы
        private void spaceCheck()
        {
            //Цикл проверки поля ввода х
            while (textBox1.Text.Contains("  "))
            {
                //Замена каждого двойного пробела одинарным
                textBox1.Text = textBox1.Text.Replace("  ", " ");
            }
            //Цикл проверки поля ввода у
            while (textBox2.Text.Contains("  "))
            {
                //Замена каждого двойного пробела одинарным
                textBox2.Text = textBox2.Text.Replace("  ", " ");
            }
        }
        //Клик по кнопке "Решить"
        private void button1_Click(object sender, EventArgs e)
        {
           //Очистка полей вывода функций
            listBox1.Text = "";
            listBox3.Text = "";
            listBox4.Text = "";
          //Вызов функции проверки полей ввода на двойные пробелы
          spaceCheck();
            //Отчистка лэйблов для вывода ошибки прир невалидных данных
            label5.Text = "";
            label6.Text = "";
            //Проверка данных на валидность
            foreach (var item in regex)
            {
                if (textBox1.Text.Contains(item.ToString()))
                {
                    label5.Text = label5.Text + "Ошибка ввода X. Удалите символ " + item.ToString() + " с индексом " + (textBox1.Text.IndexOf(item) + 1);

                }
                if (textBox2.Text.Contains(item.ToString()))
                {
                    label6.Text = label6.Text + "Ошибка ввода Y. Удалите символ " + item.ToString() + " с индексом " + (textBox2.Text.IndexOf(item) + 1);

                }
            }
            //Проверка вводимых данных на пустоту
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                label5.Text = "Отсутствуют значения x";
            }
            //Проверка вводимых данных на пустоту
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                label6.Text = "Отсутствуют значения y";
            }
            //Проверка лэйблов на пустоту и продолжение решения если они пустые, т.е. данные валидные
            if (String.IsNullOrEmpty(label5.Text) && String.IsNullOrEmpty(label6.Text))
            {
                //Считываие значения поля ввода х в массив строк, где данные разделяются почисленно пробелом
                string[] xStringArray = textBox1.Text.Split(' ');
                //Считываие значения поля ввода у в массив строк, где данные разделяются почисленно пробелом
                string[] yStringArray = textBox2.Text.Split(' ');
                //Перевод полученных массивов в тип с плавающей запятой
                float[] x = new float[xStringArray.Length];
                float[] y = new float[yStringArray.Length];
                
                //Проверка вводимых данных на одинаковую длину, чтобы значений х было столько же, сколько и у
                if (xStringArray.Length == yStringArray.Length)
                {
                    //Замена точек на , для корректного расчета (для того, чтобы не было exaption при дальнейшем рассчете и стандартизации ввода)
                    for (int i = 0; i < xStringArray.Length; i++)
                    {
                        x[i] = float.Parse(xStringArray[i].Replace('.', ','));
                        y[i] = float.Parse(yStringArray[i].Replace('.', ','));

                    }

                    float xSumm = 0;//сумма х
                    float ySumm = 0;//сумма у
                    float lnXSumm = 0;//сумма ln x
                    float ln2XSumm = 0;//сумма ln^2x
                    float lnXYSumm = 0;//сумма lnxy
                    float x2Summ = 0;//сумма x^2
                    float x3Summ = 0;//сумма x^3
                    float x4Summ = 0;//сумма x^4
                    float xySumm = 0;//сумма произведения х на у
                    float x2ySumm = 0;//сумма произведения х^2 на у
                    float fxLn = 0;//значение логарифмической функции 
                    float sqrLn = 0;//квадрат разности между значением логарифмической функции и вводимым значением у 
                    float fxLine = 0;//значение функции полинома 3 степени
                    float sqrLine = 0;//квадрат разности между значением полинома 3 степени и вводимым значением у 
                    float fxPolinom2 = 0;//значение функции полинома 2 степени
                    float sqrPolinom2 = 0;//квадрат разности между значением полинома 3 степени и вводимым значением у
                    bool lnValid = true;
                    //Итоговые значения суммы для каждого столбца
                    for (int i = 0; i < xStringArray.Length; i++)
                    {

                        xSumm = xSumm + x[i];
                        ySumm = ySumm + y[i];
                        //Проверка х на 0, если хоть 1 х равен нулю, то аппроксимацию на логарифмической функции считаем невалидной
                        if (x[i] > 0)
                        {
                            lnXSumm = lnXSumm + (float)(Math.Log(x[i]));
                            ln2XSumm = ln2XSumm + (float)(Math.Pow(Math.Log(x[i]), 2));
                            lnXYSumm = lnXYSumm + (float)(Math.Log(x[i])) * y[i];
                        }
                        else
                        {
                            lnValid = false;
                        }
                        x2Summ = x2Summ + (float)(Math.Pow(x[i], 2));
                        x3Summ = x3Summ + (float)(Math.Pow(x[i], 3));
                        x2ySumm = x2ySumm + (float)(Math.Pow(x[i], 2) * y[i]);
                        x4Summ = x4Summ + (float)(Math.Pow(x[i], 4));
                        xySumm = xySumm + x[i] * y[i];

                    }
                    //делитель, вынесен в переменную. для удобства
                    float len = x.Length;
                    
                    //Коэфициент а логарифмической функции
                    float aLn = (float)((lnXYSumm-lnXSumm* ySumm / len) /(ln2XSumm- lnXSumm*lnXSumm/ len));
                    //Коэфициент b логарифмической функции
                    float bLn = (float)((ySumm - aLn*lnXSumm) /len);
                    //Коэффициента а линейной функции
                    float aLine = (xySumm - ySumm * xSumm / len) / (x2Summ - xSumm * xSumm / len); ;
                   //Коэффициент b
                    float bLine =(ySumm-xSumm*aLine)/len;
                  //Промежуточные коэффициенты для рассчета коэффициента а полинома 2 степени
                    float a = x2ySumm - x3Summ * xySumm / (x2Summ - xSumm * xSumm / x.Length) + x3Summ * xSumm * ySumm / (x.Length * (x2Summ - xSumm * xSumm / x.Length)) - x2Summ * ySumm / x.Length + x2Summ * xSumm * xySumm / (x.Length * (x2Summ - xSumm * xSumm / x.Length)) - x2Summ * xSumm * xSumm * ySumm / (x.Length * x.Length * (x2Summ - xSumm * xSumm / x.Length));
                    float b = x4Summ - x3Summ * x3Summ / (x2Summ - xSumm * xSumm / x.Length) + x3Summ * x2Summ * xSumm / (x.Length * (x2Summ - xSumm * xSumm / x.Length)) - x2Summ * x2Summ / x.Length + x3Summ * x2Summ * xSumm / (x.Length * (x2Summ - xSumm * xSumm / x.Length)) - x2Summ * x2Summ * xSumm * xSumm / (x.Length * x.Length * (x2Summ - xSumm * xSumm / x.Length));
                    //Коэффициента а полинома 2 степени
                    float aSQR = (a) / (b);
                    //Коэффициента b полинома 2 степени
                    float bSQR = (xySumm - ySumm * xSumm / x.Length - aSQR * x3Summ + aSQR * x2Summ * xSumm / x.Length) / (x2Summ - xSumm * xSumm / x.Length);
                    //Коэффициента c полинома 2 степени
                    float cSQR = (ySumm - bSQR * xSumm - aSQR * x2Summ) / x.Length;
                    //цикл заполнения таблиц данными
                    for (int i = 0; i < xStringArray.Length; i++)
                    {
                        //Проверка на пустоту данных
                        if (x.Length != 0)
                        {
                            //Таблица с данными по лоарифмической функции
                            dataGridView1.Rows.Add(x[i], y[i], Math.Log(x[i]) ,Math.Pow( Math.Log(x[i]),2), Math.Log(x[i]) * y[i], bLn + aLn * Math.Log(x[i]), Math.Pow(bLn + aLn * Math.Log(x[i]) - y[i], 2));
                            //Лоарифмическая функция
                            fxLn = fxLn + (float)(bLn +aLn* Math.Log(x[i]));
                            //Погрешность аппроксимации логарифмической функции
                            sqrLn = (float)(sqrLn + Math.Pow(bLn + aLn * Math.Log(x[i]) - y[i], 2));
                            //Таблица с данными по функции полинома 3 степени
                            dataGridView3.Rows.Add(x[i], y[i], x[i] * x[i], x[i]*y[i],  bLine   + aLine * x[i] , sqrLine + Math.Pow((  bLine + aLine * x[i]) - y[i], 2));
                            //Лигнейная функция
                            fxLine = (float)(fxLine +  bLine  + aLine * x[i] );
                            // Погрешность аппроксимации Полинома 3 степени
                            sqrLine = (float)(sqrLine + Math.Pow(( bLine  + aLine * x[i]) - y[i], 2));
                            // Таблица с данными по функции полинома 2 степени
                            dataGridView4.Rows.Add(x[i], y[i], x[i] * x[i], x[i] * x[i] * x[i], x[i] * x[i] * x[i] * x[i], y[i] * x[i] * x[i], x[i] * y[i], aSQR * x[i] * x[i] + bSQR * x[i] + cSQR, Math.Pow(aSQR * x[i] * x[i] + bSQR * x[i] + cSQR - y[i], 2));
                            //Полином 2 степени
                            fxPolinom2 = fxPolinom2 + aSQR * x[i] * x[i] + bSQR * x[i] + cSQR;
                            // Погрешность аппроксимации Полинома 2 степени
                            sqrPolinom2 = (float)(sqrPolinom2 + Math.Pow(aSQR * x[i] * x[i] + bSQR * x[i] + cSQR - y[i], 2));
                        }
                    }
                    //заполнение последней строки в каждой изи таблиц
                    // Таблица с данными по лоарифмической функции
                    dataGridView1.Rows.Add("Итого");
                    dataGridView1.Rows.Add(xSumm, ySumm, lnXSumm, ln2XSumm,lnXYSumm, fxLn, sqrLn);
                    //Таблица с данными по функции полинома 3 степени
                    dataGridView3.Rows.Add("Итого");
                    dataGridView3.Rows.Add(xSumm, ySumm, x2Summ,xySumm, fxLine, sqrLine);
                    // Таблица с данными по функции полинома 2 степени
                    dataGridView4.Rows.Add("Итого");
                    dataGridView4.Rows.Add(xSumm, ySumm, x2Summ,x3Summ,x4Summ, x2ySumm,xySumm, fxPolinom2, sqrPolinom2);
                    // Отчистка графиков от старых построений
                    chart1.Series[0].Points.Clear();//серия 0 - точки, исходная функция
                    chart1.Series[1].Points.Clear();//серия 1 - синяя полоса, найденная функция
                    chart2.Series[0].Points.Clear();
                    chart2.Series[1].Points.Clear();
                    chart4.Series[0].Points.Clear();
                    chart4.Series[1].Points.Clear();
                    //Цикл построения графиков по точкам 
                    for (int i = 0; i < x.Length; i++)
                    {
                        //Лоарифмическая функция
                        chart1.Series[0].Points.AddXY(x[i], y[i]);
                        chart1.Series[1].Points.AddXY(x[i], aLn * Math.Log(x[i]) + bLn);
                        //Полином 3 степени
                        chart2.Series[0].Points.AddXY(x[i], y[i]);
                        chart2.Series[1].Points.AddXY(x[i],  bLine +  aLine * x[i]);
                        //Полином 2 степени
                        chart4.Series[0].Points.AddXY(x[i], y[i]);
                        chart4.Series[1].Points.AddXY(x[i], aSQR * Math.Pow(x[i], 2) + bSQR * x[i] + cSQR);
                    }
                    //проверка данных на валидность (для логарифмической функции), если данные валидны, то выводится график, иначе - надпись о невалидности данных
                    if (lnValid == true) { 
                        listBox1.Text = listBox1.Text + "f(x)=" + aLn + "*ln(x)" + ( bLn>=0? "+": "") + bLn; 
                    }
                    else { 
                        listBox1.Text = "Невозможно рассчитать аппроксимацию"; 
                    }
                    //вывод формулы полинома 3 степени
                    listBox3.Text = listBox3.Text + "f(x)=" +aLine+"x" + (bLine >= 0 ? "+" : "")+ bLine;
                    //вывод формулы полинома 2 степени
                    listBox4.Text = listBox4.Text + "f(x)=" + aSQR + "x^2"+ (bSQR >= 0 ? "+" : "") + bSQR + "x" + (cSQR >= 0 ? "+" : "") + cSQR;
                }
                //Не прошла валидация по длине строк, х и у разные, выводится сообщение о разном размере вводимых данных
                else
                {
                    //Определение в какой строке недостаточно символов
                    if (xStringArray.Length < yStringArray.Length)
                    {
                        label5.Text = "Значений х на " + (yStringArray.Length - xStringArray.Length) + " меньше, чем значений у";
                    }
                    if (xStringArray.Length > yStringArray.Length)
                    {
                        label6.Text = "Значений у на " + (xStringArray.Length - yStringArray.Length) + " меньше, чем значений х";
                    }
                }

            }
        }

    }
}
