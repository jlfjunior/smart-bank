using SmartBank.AI.Business;

namespace SmartBank.AI.UnitTests;

public class TransactionTests
{
    private Account fromAccount;
    private Account toAccount;

    public TransactionTests()
    {
        fromAccount = new Account("John Doe");
        toAccount = new Account("Jane Smith");
    }

    [Fact]
    public void Transaction_ShouldSetCorrectValues_WhenInitializedAsDeposit()
    {
        // Arrange
        var amount = 100;

        // Act
        var transaction = new Deposit(fromAccount, amount);

        // Assert
        Assert.Equal(fromAccount, transaction.FromAccount);
        Assert.Null(transaction.ToAccount);
        Assert.Equal(TransactionType.Deposit, transaction.Type);
        Assert.Equal(amount, transaction.Amount);
        Assert.True(transaction.Timestamp <= DateTime.Now);
    }
    
    [Fact]
    public void Transaction_ShouldSetCorrectValues_WhenInitializedAsWithdraw()
    {
        // Arrange
        var amount = 50m;

        // Act
        var transaction = new Withdraw(fromAccount, amount);

        // Assert
        Assert.Equal(fromAccount, transaction.FromAccount);
        Assert.Null(transaction.ToAccount);
        Assert.Equal(TransactionType.Withdraw, transaction.Type);
        Assert.Equal(amount, transaction.Amount);
        Assert.True(transaction.Timestamp <= DateTime.Now);
    }

    [Fact]
    public void Transaction_ShouldSetCorrectValues_WhenInitializedAsTransfer()
    {
        // Arrange
        var amount = 50;
        var type = TransactionType.Transfer;

        // Act
        var transaction = new Transaction(fromAccount, toAccount, type, amount);

        // Assert
        Assert.Equal(fromAccount, transaction.FromAccount);
        Assert.Equal(toAccount, transaction.ToAccount);
        Assert.Equal(type, transaction.Type);
        Assert.Equal(amount, transaction.Amount);
        Assert.True(transaction.Timestamp <= DateTime.Now);
    }

    [Fact]
    public void Transaction_ShouldThrowException_WhenInitializedAsTransferWithoutToAccount()
    {
        // Arrange
        var amount = 50m;
        var type = TransactionType.Transfer;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Transaction(fromAccount, type, amount));
    }
}