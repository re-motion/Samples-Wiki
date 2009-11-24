// Sample code for the re-motion mixins tutorial.
// This sample code illustrates the matter discussed
// on the wiki-page "re-motion mixins basics -- what about sub-classes of the target class?"

using System;
using Remotion.Implementation;
using Remotion.Mixins;
using Remotion.Reflection;

namespace WikiSamples.ParrotSubclasses
{
  public class Parrot
  {
    public virtual void Fly ()
    {
      Console.WriteLine ("Flapflap...");
    }

    public virtual void Whistle ()
    {
      Console.WriteLine ("<river kwai march here>");
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
      // you must force the .NET runtime to load a reference to the
      // Remotion.dll assembly. Otherwise the compiler will remove the
      // facilities for loading the Remotion.dll assembly and your application
      // will throw a type initializer exception, because it can't load it.
      // The easiest way to touch the assembly is to use the IMixinTarget
      // identifier:
      FrameworkVersion.RetrieveFromType (typeof (IMixinTarget));

      var myCircusParrot = ObjectFactory.Create<Parrot> (ParamList.Empty);
      Console.WriteLine ("Parrot:");
      myCircusParrot.Whistle ();                        // do as parrots do
      ((ICircusMixin) myCircusParrot).RideUniCycle ();  // show off what you have learned
      Console.WriteLine ();

      Console.WriteLine ("GreyParrot:");
      var myCircusGreyParrot = ObjectFactory.Create<GreyParrot> (ParamList.Empty);
      myCircusGreyParrot.DestroyFurniture ();               // grey parrots LOVE destruction
      ((ICircusMixin) myCircusGreyParrot).RideUniCycle ();  // show off what you have learned from your ancestor
      Console.WriteLine ();

      Console.WriteLine ("Macaw:");
      var myCircusMacaw = ObjectFactory.Create<Macaw> (ParamList.Empty);
      myCircusMacaw.CrackNut ();                            // for a macaw, a nut is like a zip-loc; keep hands and feet clear!
      ((ICircusMixin) myCircusMacaw).RideUniCycle ();       // show off what you have learned from your ancestor
      Console.WriteLine ();

      Console.ReadLine ();
    }
  }
}
