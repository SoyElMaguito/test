
using System.Timers;

namespace FarmApp;

public partial class MainPage : ContentPage
{
	static Farm farm = new(1000);
	static Grid gCow;
	static Grid gInfo;
	static int hours = 0;
	static Label lbHours = new();
	static Label lbSellMilk = new();
	static Label lbCows = new();
	static Label lbFood = new();
	static Label lbLimitCows = new();
	static Label lbWorkingHours = new();
	static Label lbMoney = new();
	static Label lbCollectMilk = new();
	static Label lbWorkedPrice = new();
	static Label lbFoodPrice = new();
	static Label lbPayDay = new();

	static string cowimage = "cow.png";




	public MainPage()
	{
		InitializeComponent();
		farm.buyCow();
		gCow = GCow;
		gInfo = GInfo;
		interfaceGenerator();
		updateStatus();
		setTimer();

	}

	static void updateStatus()
	{
		var dispatcher = Application.Current.Dispatcher;

		dispatcher.Dispatch(() =>
		{
			Dictionary<string, object> status = farm.getStatus();
			lbMoney.Text = "Dinero: $" + status["money"].ToString();
			lbSellMilk.Text = "Precio de venta: $" + status["milkPrice"];
			lbWorkedPrice.Text = "costo hora de trabajo: $" + status["workedPrice"];
			lbFoodPrice.Text = "Precio de la comida : $" + status["foodPrice"];
			lbLimitCows.Text = "Limite: " + status["limitCows"].ToString();
			lbCows.Text = "Vacas: " + status["countCows"].ToString();
			lbFood.Text = "Comida: " + status["cowFood"];
			lbWorkingHours.Text = "Horas de Trabajo: " + status["workingHours"];
			lbHours.Text = "Tics: " + hours;
			lbCollectMilk.Text = "Leche Recolectada: " + status["milkCollected"];
			lbPayDay.Text = "Pago del trabajo al chalan: $" + status["payDay"];


			showCow();
		});

	}

	static void clickBuyCow(object sender, EventArgs e)
	{
		farm.buyCow();
		updateStatus();
	}
	static void clickFeedCow(object sender, EventArgs e)
	{
		farm.cowFeed();
		updateStatus();
	}
	static void clickSellMilk(object sender, EventArgs e)
	{
		farm.sellMilk();
		updateStatus();
	}
	static void clickBuyFood(object sender, EventArgs e)
	{
		farm.buyFood();
		updateStatus();
	}
	static void clickCollectMilk(object sender, EventArgs e)
	{
		farm.collectMilk();
		updateStatus();
	}
	static void ClickPayDay(object sender, EventArgs e)
	{
		farm.payDay();
		updateStatus();
	}
	static void showCow()
	{
		int x = 0, y = 0;
		var dispatcher = Application.Current.Dispatcher;

		dispatcher.Dispatch(() =>
		{
			if (MainPage.gCow != null)
			{
				gCow.Clear();
			}
		});
		foreach (Cow cow in farm.cowStatus())
		{
			VerticalStackLayout vertical = new();
			Image image = new();
			Label label = new();
			if (cow.Hungry >= 2)
				cowimage = "cow_eat.gif";
			else
				cowimage = "cow.png";
			image.Source = cowimage;
			image.HeightRequest = 150;
			label.Text = "Alimento: " + cow.Hungry + " Leche: " + cow.Milk;
			label.HorizontalTextAlignment = TextAlignment.Center;
			vertical.Add(image);
			vertical.Add(label);
			dispatcher.Dispatch(() =>
		   {
			   if (gCow != null)
			   {
				   gCow.Add(vertical, x, y);
				   if (y == 2)
				   {
					   y = 0;
					   x++;
				   }
				   else
				   {
					   y++;
				   }
			   }
		   });


		}


	}
	public void setTimer()
	{
		System.Timers.Timer aTimer = new System.Timers.Timer();
		aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		aTimer.Interval = 4000; // ~ 5 seconds
		aTimer.Enabled = true;
	}
	static void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		farm.simulate();
		hours++;
		updateStatus();
	}

	private static void interfaceGenerator()
	{

		StackLayout layout = new();
		VerticalStackLayout verticalLayout = new();
		Button btnBuyFood = new();
		Button btnSellMilk = new();
		Button btnBuyCow = new();
		Button btnFeedCow = new();
		Button btnCollectMilk = new();
		Button btnPayDay = new();

		layout.Margin=10;
		verticalLayout.Spacing = 3;
		verticalLayout.Margin = 10;
		btnBuyFood.Text = "+Comida";
		btnSellMilk.Text = "VenderLeche";
		btnBuyCow.Text = "+Vacas";
		btnFeedCow.Text = "Alimentar";
		btnCollectMilk.Text = "Recolectar leche";
		btnPayDay.Text = "Pagar al chalan";
		btnFeedCow.Clicked += clickFeedCow;
		btnBuyCow.Clicked += clickBuyCow;
		btnSellMilk.Clicked += clickSellMilk;
		btnBuyFood.Clicked += clickBuyFood;
		btnCollectMilk.Clicked += clickCollectMilk;
		btnPayDay.Clicked += ClickPayDay;

		verticalLayout.Add(btnBuyFood);
		verticalLayout.Add(btnSellMilk);
		verticalLayout.Add(btnBuyCow);
		verticalLayout.Add(btnFeedCow);
		verticalLayout.Add(btnCollectMilk);
		verticalLayout.Add(btnPayDay);
		layout.Add(lbMoney);
		layout.Add(lbFoodPrice);
		layout.Add(lbWorkedPrice);
		layout.Add(lbPayDay);
		layout.Add(lbSellMilk);
		layout.Add(lbLimitCows);
		layout.Add(lbCows);
		layout.Add(lbFood);
		layout.Add(lbWorkingHours);
		layout.Add(lbHours);
		layout.Add(lbCollectMilk);

		var dispatcher = Application.Current.Dispatcher;

		dispatcher.Dispatch(() =>
		{
			if (gInfo != null)
			{
				gInfo.Add(verticalLayout, 0, 0);
				gInfo.Add(layout, 1, 0);
			}
		});
	}


}

