namespace trabalho.models
{
    public class validacaoCpf
    {
        public static bool ValidaCPF(string cpf)
        {
            cpf = cpf.Replace(".", "");
            cpf = cpf.Replace("-", "");

            int[] mult1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;
            int resto1;
            int resto2;

            if (cpf.Length != 11)
            {
                return false;
            }

            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * mult1[i];
            }

            resto1 = soma % 11;

            if (resto1 < 2)
            {
                resto1 = 0;
            }

            else
            {
                resto1 = 11 - resto1;
            }


            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * mult2[i];
            }

            resto2 = soma % 11;

            if (resto2 < 2)
            {
                resto2 = 0;
            }

            else
            {
                resto2 = 11 - resto2;
            }


            if (resto1 != int.Parse(cpf[9].ToString()) || resto2 != int.Parse(cpf[10].ToString()))
            {
                return false;
            }

            return true;
        }

    }
}
