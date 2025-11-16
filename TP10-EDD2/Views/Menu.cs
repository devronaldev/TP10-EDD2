using TP10_EDD2.Controllers;
using TP10_EDD2.Models;

namespace TP10_EDD2.Views;

/// <summary>
/// Manages the user interface (console menu) for the medicine inventory application.
/// </summary>
/// <remarks>
/// PT-BR: Gerencia a interface do usuário (menu de console) para a aplicação de inventário de medicamentos.
/// </remarks>
public class Menu
{
    /// <summary>
    /// The controller instance used to interact with the medicine data.
    /// </summary>
    /// <remarks>
    /// PT-BR: A instância do controlador usada para interagir com os dados dos medicamentos.
    /// </remarks>
    private MedicineController medicineController = new MedicineController();
    
    /// <summary>
    /// Starts the main application loop, continuously displaying the options menu.
    /// </summary>
    /// <remarks>
    /// PT-BR: Inicia o loop principal da aplicação, exibindo continuamente o menu de opções.
    /// </remarks>
    public void Init()
    {
        while (true)
        {
            ShowOptions();    
        }
    }

    /// <summary>
    /// Clears the console, displays all available menu options, and waits for user input.
    /// </summary>
    /// <remarks>
    /// PT-BR: Limpa o console, exibe todas as opções de menu disponíveis e aguarda a entrada do usuário.
    /// </remarks>
    private void ShowOptions()
    {
        Console.Clear();
        Console.WriteLine("0. Finalizar Processo.");
        Console.WriteLine("1. Cadastrar Medicamento.");
        Console.WriteLine("2. Consultar Medicamento Sintético.");
        Console.WriteLine("3. Consultar Medicamento Análitico.");
        Console.WriteLine("4. Comprar Medicamento (Cadastrar Lote).");
        Console.WriteLine("5. Vender Medicamento.");
        Console.WriteLine("6. Listar Todos Medicamentos.");

        SwitchOptions(ReadInt("Insira o número da opção: ", 0, 6));
    }

    /// <summary>
    /// Routes the user to the appropriate method based on the selected menu option.
    /// </summary>
    /// <param name="option">The numeric option selected by the user.</param>
    /// <remarks>
    /// PT-BR: Direciona o usuário para o método apropriado com base na opção de menu selecionada.
    /// <para>PT-BR (option): A opção numérica selecionada pelo usuário.</para>
    /// </remarks>
    private void SwitchOptions(int option)
    {
        switch (option)
        {
            case 0: Environment.Exit(0); break;
            case 1: AddMedicine(); break;
            case 2: SearchMedicine(1); break;
            case 3: SearchMedicine(2); break;
            case 4: AddBatch(); break;
            case 5: SellBatch(); break;
            case 6: ShowBatches(); break;
            default:
                // O ReadInt(0, 6) já trata isso, mas é uma boa prática manter o default.
                Console.WriteLine("Valor inválido. As opções vão de 0 a 6.");
                ClickAnyKey();
                break;
        }
    }

    /// <summary>
    /// Handles the workflow for registering a new medicine by prompting for its name and laboratory.
    /// </summary>
    /// <remarks>
    /// PT-BR: Gerencia o fluxo de trabalho para cadastrar um novo medicamento, solicitando seu nome e laboratório.
    /// </remarks>
    private void AddMedicine()
    {
        Console.Write("Insira o nome do Medicamento: ");
        var medicineName = Console.ReadLine();
        
        Console.Write("Insira o nome do laboratório: ");
        var labName = Console.ReadLine();
        
        var newMedicine = new Medicine(medicineName, labName);
        
        medicineController.AddMedicine(newMedicine);
        Console.WriteLine("Medicamento adicionado com sucesso!");
        ClickAnyKey();
    }

    /// <summary>
    /// Searches for a specific medicine by ID and displays its data based on the chosen format (synthetic or analytic).
    /// </summary>
    /// <param name="option">The display format (1 for synthetic, 2 for analytic/detailed).</param>
    /// <remarks>
    /// PT-BR: Procura por um medicamento específico pelo ID e exibe seus dados com base no formato escolhido (sintético ou analítico).
    /// <para>PT-BR (option): O formato de exibição (1 para sintético, 2 para analítico/detalhado).</para>
    /// </remarks>
    private void SearchMedicine(int option)
    {
        var id = ReadInt("Insira o id do Medicamento: ");
        
        var medicine = medicineController.SearchMedicine(id);
        if (medicine == null)
        {
            Console.WriteLine("Medicamento não encontrado!\nVerifique se ele existe na lista.");
            ClickAnyKey();
            return;
        }

        switch (option)
        {
            case 1: Console.WriteLine(medicine.ToString()); break;
            case 2: Console.WriteLine(medicine.ShowBatches()); break;
            default: Console.WriteLine("Um erro inesperado ocorreu!"); break;
        }
        
        ClickAnyKey();
    }

    /// <summary>
    /// Handles the workflow for buying medicine, which involves adding a new batch (quantity and expiry date) to an existing medicine.
    /// </summary>
    /// <remarks>
    /// PT-BR: Gerencia o fluxo de trabalho para comprar um medicamento, o que envolve adicionar um novo lote (quantidade e data de validade) a um medicamento existente.
    /// </remarks>
    private void AddBatch()
    {
        var id = ReadInt("Insira o id do Medicamento: ");
        
        var medicine = medicineController.SearchMedicine(id);
        if (medicine == null)
        {
            Console.WriteLine("Medicamento não encontrado!\nVerifique se ele existe na lista.");
            ClickAnyKey();
            return;
        }

        var qty = ReadInt("Insira a quantidade: ");
        var date = ReadDateTime();
        medicine.BuyBatch(new Batch(qty, date));
        Console.WriteLine("Lote adicionado com sucesso!");
        ClickAnyKey();
    }

    /// <summary>
    /// Handles the workflow for selling medicine, prompting for an ID and quantity, and reporting the transaction status.
    /// </summary>
    /// <remarks>
    /// PT-BR: Gerencia o fluxo de trabalho para vender um medicamento, solicitando um ID e quantidade, e informando o status da transação.
    /// </remarks>
    private void SellBatch()
    {
        var id = ReadInt("Insira o id do Medicamento: ");
        var medicine = medicineController.SearchMedicine(id);
        if (medicine == null)
        {
            Console.WriteLine("Medicamento não encontrado!\nVerifique se ele existe na lista.");
            ClickAnyKey();
            return;
        }
        
        var qty =  ReadInt("Insira a quantidade desejada: ");
        if (medicine.SellMedicine(qty))
        {
            Console.WriteLine("Medicamento Vendido com sucesso!");
            ClickAnyKey();
            }
        else
        {
            Console.WriteLine("Não há a quantidade desejada!");
            ClickAnyKey();
        }
    }

    /// <summary>
    /// Displays a complete report of all medicines and their corresponding batches.
    /// </summary>
    /// <remarks>
    /// PT-BR: Exibe um relatório completo de todos os medicamentos e seus lotes correspondentes.
    /// </remarks>
    private void ShowBatches()
    {
        Console.WriteLine(medicineController.ToString());
        ClickAnyKey();
    }

    /// <summary>
    /// Utility method that pauses the console execution until the user presses any key.
    /// </summary>
    /// <remarks>
    /// PT-BR: Método utilitário que pausa a execução do console até que o usuário pressione qualquer tecla.
    /// </remarks>
    private void ClickAnyKey()
    {
        Console.WriteLine("Clique em qualquer tecla para retornar ao menu.");
        Console.ReadKey();
    }
    
    /// <summary>
    /// Robustly reads an integer from the console, validating that it is a number and within the specified range.
    /// </summary>
    /// <param name="message">The prompt message to display to the user.</param>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <returns>A valid integer entered by the user.</returns>
    /// <remarks>
    /// PT-BR: Lê de forma robusta um inteiro do console, validando que é um número e que está dentro do intervalo especificado.
    /// <para>PT-BR (message): A mensagem de prompt a ser exibida ao usuário.</para>
    /// <para>PT-BR (min): O valor mínimo permitido (inclusivo).</para>
    /// <para>PT-BR (max): O valor máximo permitido (inclusivo).</para>
    /// <para>PT-BR (returns): Um inteiro válido inserido pelo usuário.</para>
    /// </remarks>
    private int ReadInt(string message, int min = 0, int max = 999)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out var value))
            {
                if (value >= min && value <= max)
                {
                    return value; 
                }
                else
                {
                    Console.WriteLine($"Valor inválido. O número deve estar entre {min} e {max}.");
                }
            }
            else
            {
                Console.WriteLine("Valor inválido! Digite apenas números.");
            }
        
            Console.WriteLine("Tente novamente.");
        }
    }

    /// <summary>
    /// Robustly reads a <c>DateTime</c> from the console by prompting for year, month, and day.
    /// Validates leap years and the correct number of days in the month.
    /// </summary>
    /// <returns>A valid <c>DateTime</c> object.</returns>
    /// <remarks>
    /// PT-BR: Lê de forma robusta um <c>DateTime</c> do console, solicitando ano, mês e dia.
    /// Valida anos bissextos e o número correto de dias no mês.
    /// <para>PT-BR (returns): Um objeto <c>DateTime</c> válido.</para>
    /// </remarks>
    private DateTime ReadDateTime()
    {
        var year = ReadInt("Insira o ano de vencimento: ", 2025, 2999);
    
        var month =  ReadInt("Insira o mês de vencimento: ", 1, 12);
    
        int maxDay = DateTime.DaysInMonth(year, month);
    
        var day = ReadInt($"Insira o dia de vencimento: ", 1, maxDay);
    
        return new DateTime(year, month, day);
    }
}