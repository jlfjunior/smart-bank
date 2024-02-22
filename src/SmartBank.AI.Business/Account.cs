namespace SmartBank.AI.Business;

public class Account
{
    public Account(string owner) : this() => (Owner, Number) = (owner, Guid.NewGuid());
    private Account() => _transactions = new List<Transaction>();

    public Guid Number { get; }
    public string Owner { get; private set; }
    public decimal Balance { get; private set; }
    
    private List<Transaction> _transactions;

    public void Deposit(decimal amount)
    {
        if (amount <= decimal.Zero) throw new InvalidAmountException();
        
        var deposit = new Deposit(this, amount);
        
        _transactions.Add(deposit);
        
        Balance += amount;
    }
    
    public void Withdraw(decimal amount)
    {
        if (amount <= decimal.Zero) throw new InvalidAmountException();
        if (Balance < amount) throw new NegativeBalanceException();

        var withdraw = new Withdraw(this, amount);

        _transactions.Add(withdraw);

        Balance -= amount;
    }

    public override string ToString()
    {
        return $"Account: {Number} - Owner: {Owner} - Balance: {Balance}";
    }
}

public class Deposit(Account account, decimal amount) : Transaction(account, TransactionType.Deposit, amount);
public class Withdraw(Account account, decimal amount) : Transaction(account, TransactionType.Withdraw, amount);

public class Transaction
{
    public Transaction(Account account, TransactionType type, decimal amount) 
        => (FromAccount, Type, Amount) = (account, type, amount);

    public Transaction(Account fromAccount, Account toAccount, TransactionType type, decimal amount)
        : this(fromAccount, TransactionType.Transfer, amount) => (ToAccount) = (toAccount);

    public Account FromAccount { get; }
    public Account ToAccount { get; }
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public DateTime Timestamp { get; } = DateTime.Now;
    
    public override string ToString()
    {
        return $"Account: {FromAccount} Type: {nameof(Type)} - Date: {Timestamp} - Amount: {Amount}";
    }
}

public enum TransactionType
{
    Deposit,
    Withdraw,
    Transfer
}

public class NegativeBalanceException() : InvalidOperationException(message: "Account balance violated.");

public class InvalidAmountException() : ArgumentException(message: "Amount cannot be lass or equal zero.");
