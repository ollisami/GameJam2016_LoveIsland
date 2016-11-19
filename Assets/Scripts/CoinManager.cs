using System;

public class CoinManager
{
	private int coinCount = 0;
	public static readonly CoinManager Instance = new CoinManager();
	private CoinManager ()
	{
	}

	public int CoinCount {
		get { return coinCount; }
	}

	public void AddCoins(int coins) {
		coinCount += coins;
	}

	public bool UseCoins(int coins) {
		if (this.coinCount < coins) {
			return false; // not enough coins
		}
			
		this.coinCount -= coins;
		return true;
	}
}

