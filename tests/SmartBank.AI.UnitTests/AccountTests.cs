using SmartBank.AI.Business;

namespace SmartBank.AI.UnitTests;

public class AccountTests
{
    private Account account = new("John Doe");

    [Fact]
    public void Deposit_ShouldIncreaseBalance_WhenAmountIsPositive()
    {
        // Arrange
        var initialBalance = account.Balance;
        var depositAmount = 100;

        // Act
        account.Deposit(depositAmount);

        // Assert
        Assert.Equal(initialBalance + depositAmount, account.Balance);
    }

    [Fact]
    public void Deposit_ShouldThrowException_WhenAmountIsZeroOrNegative()
    {
        // Arrange
        var depositAmount = 0;

        // Act & Assert
        Assert.Throws<InvalidAmountException>(() => account.Deposit(depositAmount));
    }

    [Fact]
    public void Withdraw_ShouldDecreaseBalance_WhenAmountIsLessThanBalance()
    {
        // Arrange
        account.Deposit(100);
        var initialBalance = account.Balance;
        var withdrawalAmount = 50;

        // Act
        account.Withdraw(withdrawalAmount);

        // Assert
        Assert.Equal(initialBalance - withdrawalAmount, account.Balance);
    }

    [Fact]
    public void Withdraw_ShouldThrowException_WhenAmountIsZeroOrNegative()
    {
        // Arrange
        var withdrawalAmount = 0;

        // Act & Assert
        Assert.Throws<InvalidAmountException>(() => account.Withdraw(withdrawalAmount));
    }

    [Fact]
    public void Withdraw_ShouldThrowException_WhenAmountIsGreaterThanBalance()
    {
        // Arrange
        account.Deposit(100);
        var withdrawalAmount = 150;

        // Act & Assert
        Assert.Throws<NegativeBalanceException>(() => account.Withdraw(withdrawalAmount));
    }
}