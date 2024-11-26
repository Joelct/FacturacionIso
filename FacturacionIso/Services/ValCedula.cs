using FacturacionIso.Services;

public class ValCedula : ICedulaValidator
{
    public bool ValidateCedula(string pCedula)
    {
        int vnTotal = 0;
        string vcCedula = pCedula.Replace("-", "");
        int pLongCed = vcCedula.Trim().Length;
        int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

        if (pLongCed != 11)
            return false;

        for (int vDig = 1; vDig <= pLongCed; vDig++)
        {
            int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
            vnTotal += vCalculo < 10 ? vCalculo : (vCalculo / 10 + vCalculo % 10);
        }

        return vnTotal % 10 == 0;
    }
}

