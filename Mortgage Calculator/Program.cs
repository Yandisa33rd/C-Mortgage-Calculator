using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

class MortgageCalculator
{ //Declare variables to be accessible to all class
    public static int loanTermYears;
    public static double LoanAmount;
    public static double annualInterestRate;
    public static double monthlyPayment;
    public static double totalInterest;
    public static double totalAmount;
    public static void Main(string[] args)
    { //Create menu
        Console.WriteLine("Welcome to the mortgage menu\n" + //Menu options
            "Choose an option\n");
        Console.WriteLine("1.Calculate Monthly repayment\n" +
            "2.Calculate interest\n" +
            "3.Amount to be paid over time\n" +
            "4.Payment schedule\n" + "5.Exit\n");

        try //Try catch for error handling 
        {
        int option = Convert.ToInt32(Console.ReadLine()); //functions for each menu option
        if (option == 1) { 
            CalculateMonthlyRepayment();
            Main(args);
        }
        else if (option == 2)
        {
            CalculateTotalInterestPaid();
            Main(args);
        }
        else if (option == 3)
        {
            CalculateTotalAmountPaid();
            Main(args);
        }
        else if (option == 4)
            {
                GenerateAmortizationSchedule();
                Main(args);
            }
        else if (option == 5)
            {
                Environment.ExitCode = 5;
            }
        }
        catch(FormatException)
        {
            Console.WriteLine("Please enter valid input\n");
            Main(args);
        }
        
    }
    public static void CalculateMonthlyRepayment() //Calculate the Monthly repayment
    {
        try
        {
            do
            {
                Console.WriteLine("Enter loan amount");
                LoanAmount = Convert.ToDouble(Console.ReadLine());

                if (LoanAmount <= 0)
                {
                    Console.WriteLine("Enter value greater then 0\n");
                }
            }
            while (LoanAmount <= 0); //Do while to make sure the user does not enter 0

            do
            {
            Console.WriteLine("What will your annual interest rate be?");
                annualInterestRate = Convert.ToDouble(Console.ReadLine());
                if(annualInterestRate <= 0)
                {
                    Console.WriteLine("Interest may not be 0!\n");
                }
            }
            while(annualInterestRate <= 0); //Do while to make sure the user does not enter 0


            do
            {
            Console.WriteLine("In how many years will the loan be payed off?");
            loanTermYears = Convert.ToInt32(Console.ReadLine());
                if (loanTermYears <= 0)
                {
                    Console.WriteLine("Payment period may not be less than a year\n");
                }
            }
            while(loanTermYears <= 0); //Do while to make sure the user does not enter 0


            Console.WriteLine($"Your annual interest rate will be {annualInterestRate}%");
        monthlyPayment = (LoanAmount + LoanAmount * annualInterestRate / 100) / 12;

        Console.WriteLine($"Your monthly payment will be R{monthlyPayment} for {loanTermYears * 12} months\n");
        }
        catch(FormatException)
        {
            Console.WriteLine("Please enter a valid input");
            CalculateMonthlyRepayment(); //Points out data type errors and reruns function 
        }
    }

    public static void CalculateTotalInterestPaid() //Calculate how much interest is in the loan
    {
        totalInterest = LoanAmount * annualInterestRate / 100 * loanTermYears;
        Console.WriteLine($"The total interest of the loan will be:  R{totalInterest}") ;
    }
    public static void CalculateTotalAmountPaid() //Find the total amount of the loan
    {
        totalAmount = LoanAmount + LoanAmount * annualInterestRate / 100 * loanTermYears;
        Console.WriteLine($"The total amount of the loan will be: R{totalAmount}");
    }
    public static void GenerateAmortizationSchedule() //Payment schedule for the loan in detail
    {
        double remainingBalance = LoanAmount;
        double monthlyInterestRate = annualInterestRate / 100 / 12;
        int totalPayments = loanTermYears * 12;

        Console.WriteLine("\nAmortization Schedule:");

        Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-20}",
                          "Payment", "Payment Amount", "Interest Paid", "Principal Paid", "Remaining Balance");

        for (int paymentNumber = 1; paymentNumber <= totalPayments; paymentNumber++)
        {
            double interestPaid = remainingBalance * monthlyInterestRate;
            double principalPaid = monthlyPayment - interestPaid;
            remainingBalance -= principalPaid;

            Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-20}",
                paymentNumber, "R" + monthlyPayment.ToString("F2"), "R" + interestPaid.ToString("F2"), "R" + principalPaid.ToString("F2"),
                "R" + remainingBalance.ToString("F2"));

            if (remainingBalance <= 0)
                break;
        }
    }
}