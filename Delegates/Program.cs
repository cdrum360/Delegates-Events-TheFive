using System;

delegate void Handler();        //  Declare the delegate (1)

#region Publisher
class Incrementer
{
   public event Handler CountedADozen;      // Create(Declare) and publish an event (3) 

   public void DoCount()
   {
      for ( int i=1; i < 100; i++ )
         if ( i % 12 == 0 && CountedADozen != null )
            CountedADozen();        // Raise(Fire) the event every 12 counts (5); raising the event invokes the delegate
   }
}
#endregion

#region Subscriber
class Dozens
{
   public int DozensCount { get; private set; }

   public Dozens( Incrementer incrementer )
   {
        DozensCount = 0;
        incrementer.CountedADozen += IncrementDozensCount;      // Subscribe to the event by adding an event handler to an event  - (delegate form) (4) 
//        incrementer.CountedADozen += () => DozensCount++;       // Subscribe to the event  - (lamdba form)
        
   }

   #region EventHandler
   void IncrementDozensCount()      // Declare the event handler (2) 
   {
      DozensCount++;
   }
   #endregion

}
#endregion

#region Main
class Program
{
   static void Main()
   {
      Incrementer incrementer = new Incrementer();          // instantiate Publisher
      Dozens dozensCounter    = new Dozens( incrementer );  // instantiate Subscriber

      incrementer.DoCount();    // invoke method in Publisher, which will raise event.
      Console.WriteLine( "Number of dozens = {0}",
                              dozensCounter.DozensCount );
      Console.ReadLine();
   }
}
#endregion