// Sample code for the re-motion mixins tutorial.
// This sample code illustrates the matter discussed
// on the wiki-page "advanced re-motion mixins -- removing items, scoping of mixin configurations"

using System;
using Remotion.Mixins;
using Remotion.Reflection;

namespace WikiSamples.ParrotSuppressMacaw
{
  // target class(es)
  public class Parrot
  {
    public virtual void Fly ()
    {
      Console.WriteLine ("Flapflap...");
    }

    public virtual void Whistle ()
    {
      Console.WriteLine ("<River Kwai March here>");
    }

    public virtual void Say (string s)
    {
      Console.WriteLine ("\"{0}\"", s);
    }
  }

  public class GreyParrot : Parrot
  {
    public virtual void DestroyFurniture ()
    {
      Console.WriteLine ("Destroying furniture...");
    }
  }

  public class Macaw : Parrot
  {
    public virtual void CrackNut ()
    {
      Console.WriteLine ("Cracking nut...");
    }
  }

  public interface ICircusMixin
  {
    void RideUniCycle ();
  }

  [Extends (typeof (Parrot))]
  public class CircusMixin : ICircusMixin
  {
    public void RideUniCycle ()
    {
      Console.WriteLine ("I'm so amazing! I'm riding a unicycle!");
    }
  }

  class Program
  {
    static void Main (string[] args)
    {
      // mind the loading of 'Remotion.dll'!
      var t = typeof (IMixinTarget);

      var myMixinConfiguration = MixinConfiguration.BuildFromActive ()   // clone the currently active configuration 
        // (that happens to be the master default config 
        // at this point)
        .ForClass<Macaw> ().SuppressMixin<CircusMixin> ()                // remove the mixing of the circus mixin from the 'Macaw' class
        .BuildConfiguration ();                                          // cause the actual building of our clonee 

      // now we span a scope for our special configuration
      using (myMixinConfiguration.EnterScope ())
      {
        // The mixed parrot class works as usual, no suppression here
        var myCircusParrot = ObjectFactory.Create<Parrot> (ParamList.Empty);
        Console.WriteLine ("Parrot:");
        myCircusParrot.Whistle ();
        ((ICircusMixin) myCircusParrot).RideUniCycle ();
        Console.WriteLine ();

        // The mixed grey parrot class works as usua, no suppression here either
        Console.WriteLine ("GreyParrot:");
        var myCircusGreyParrot = ObjectFactory.Create<GreyParrot> (ParamList.Empty);
        myCircusGreyParrot.DestroyFurniture ();
        ((ICircusMixin) myCircusGreyParrot).RideUniCycle ();
        Console.WriteLine ();

        // The macaw can do as macaws do (crack nuts)...
        Console.WriteLine ("Macaw:");
        var myBogusCircusMacaw = ObjectFactory.Create<Macaw> (ParamList.Empty);
        myBogusCircusMacaw.CrackNut ();
        Console.WriteLine ("unicycling...");
        try
        {
          // ... but it can't ride the unicycle, because that's been
          // suppressed
          ((ICircusMixin) myBogusCircusMacaw).RideUniCycle ();
        }
        catch (InvalidCastException)
        {
          // ... what gives us a bummer at run-time
          Console.WriteLine ("... or not. We caught an 'InvalidCastException'.");
          Console.WriteLine ("The 'Macaw' class can't be cast to 'ICircusMixin'");
          Console.WriteLine ("because we suppressed that mixin. (Remember?)");
        }

        Console.WriteLine ();
      } // end of scope with the non-unicycle macaw

      // at this point, the original (default master) configuration
      // is active again, so the macaw CAN ride the unicycle
      Console.WriteLine ("Macaw:");
      var myWorkingCircusMacaw = ObjectFactory.Create<Macaw> (ParamList.Empty);
      myWorkingCircusMacaw.CrackNut ();
      ((ICircusMixin) myWorkingCircusMacaw).RideUniCycle ();
      Console.WriteLine ();

      Console.ReadLine ();
    }
  }
}
