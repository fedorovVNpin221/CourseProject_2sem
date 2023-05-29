using System.Security.Cryptography.X509Certificates;

namespace курсач_орел_и_решка_ИИ
{
    public partial class Form1 : Form
    {
        public int count = 0, PC_wins = 0, user_wins = 0;
        public double PC_wins_percent = 0, user_wins_percent = 0;
        public int eagle = 1, tails = 0;
        public Form1()
        {
            InitializeComponent();
            label5.Text = "...";
            label8.Text = "...";
            label14.Text = user_wins_percent.ToString() + "%";
            label16.Text = PC_wins_percent.ToString() + "%";
            label10.Text = user_wins.ToString();
            label12.Text = PC_wins.ToString();
        }

        private void button1_Click(object sender, EventArgs e) // кнопка "орёл" для игрока
        {
            button2.Enabled = false;
            button5.Enabled = true;
            if (PC_wins_percent < 50)
            {
                randomizer(); // запуск рандомайзера, если компьютер проигрывает
            }
            else
            {
                if (button1.Enabled) // запуск нейрона, если компьютер выигрывает
                {
                    int prediction = NeuralpredictCoinFlip(eagle);
                    button4.Enabled = prediction == 1;
                    button3.Enabled = prediction == 0;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // кнопка "решка" для игрока
        {
            button1.Enabled = false;
            button5.Enabled = true;
            if (PC_wins_percent < 50) // запуск рандомайзера, если компьютер проигрывает
            {
                randomizer(); 
            }
            else
            {
                if (button2.Enabled) // запуск нейрона, если компьютер выигрывает
                {
                    int prediction = NeuralpredictCoinFlip(tails);
                    button4.Enabled = prediction == 1;
                    button3.Enabled = prediction == 0;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            this.MinimumSize = new Size(850, 400); // минимальный размер окна
            this.Text = "Орёл и решка"; // название формы
        }

        private void button5_Click(object sender, EventArgs e) // кнопка "Подкинуть монетку"
        {
            Random rnd = new Random();
            int coin = rnd.Next(0, 2);
            button6.Enabled = true;
            if (coin == 1)
            {
                label5.Text = "Орёл";
            }
            else if (coin == 0)
            {
                label5.Text = "Решка";
            }
            button5.Enabled = false;
        }

        private void randomizer() // рандомайзер противника пользователя
        {
            Random rnd = new Random();
            int num = rnd.Next(0, 2);
            if (num == 1)
            {
                button3.Enabled = true;
                button4.Enabled = false;
            }
            else if (num == 0)
            {
                button3.Enabled = false;
                button4.Enabled = true;
            }
        }

        private void button6_Click_1(object sender, EventArgs e) // кнопка "Попробовать еще раз"
        {
            count++;
            label8.Text = count.ToString();
            button6.Enabled = false;

            if (button1.Enabled && button3.Enabled && (label5.Text == button1.Text)) // выигрыш ИИ и пользователя(орёл)
            {
                user_wins++;
                PC_wins++;
                label10.Text = user_wins.ToString();
                label12.Text = PC_wins.ToString();
            }
            else if (button2.Enabled && button4.Enabled && (label5.Text == button2.Text)) // выигрыш ИИ и пользователя(решка)
            {
                user_wins++;
                PC_wins++;
                label10.Text = user_wins.ToString();
                label12.Text = PC_wins.ToString();
            }
            else if (button1.Enabled && button1.Text == label5.Text || button2.Enabled && button2.Text == label5.Text) // выигрыш пользователя
            {
                user_wins++;
                label10.Text = user_wins.ToString();
            }
            else if (button3.Enabled && button3.Text == label5.Text || button4.Enabled && button4.Text == label5.Text) // выигрыш ИИ
            {
                PC_wins++;
                label12.Text = PC_wins.ToString();
            }

            label5.Text = "...";
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = false;

            user_wins_percent = (100 * user_wins) / count;
            PC_wins_percent = (100 * PC_wins) / count;
            label14.Text = user_wins_percent.ToString() + "%";
            label16.Text = PC_wins_percent.ToString() + "%";
        }

        public static int NeuralpredictCoinFlip(double input) // функция нейрона
        {
            Random rnd = new Random();
            double weight = rnd.NextDouble() * (0.9 + 0.2) - 0.2; // случайная инициализация веса
            double bias = rnd.NextDouble() * (0.1 + 0.0001) - 0.0001; ; // случайная инициализация смещения
            double learningRate = 0.01; 
            int[] inputs = { 1, 1, 1, 1 }; // возможные входные значения
            int[] targets = { 1, 1, 1, 0 }; // правильные ответы для каждого входного значения
            for (double i = 0; i < 100000; i++) 
            {
                for (int j = 0; j < inputs.Length; j++) 
                {
                    double weightedInput1 = inputs[j] * weight; // вычисляем взвешенный входной сигнал
                    double sum1 = weightedInput1 + bias; // вычисляем сумму входных значений и смещения
                    int output = 0;
                    if (sum1 >= 0.5)
                        output = 0;
                    else
                        output = 1;
                    int error = targets[j] - output; // вычисляем ошибку
                    weight += error * inputs[j] * learningRate; // корректируем вес
                    bias += error * learningRate; // корректируем смещение
                }
            }
            double weightedInput = input * weight; // вычисляем взвешенный входной сигнал
            double sum = weightedInput + bias; // вычисляем сумму входных значений и смещения
            if (sum >= 0.5)
            {
                return 0; // если результат больше или равен 0.5, то нейрон предсказывает "решку"
            }
            else
            {
                return 1; // если результат меньше 0.5, то нейрон предсказывает "орла"
            }
        }
    }
}

