// Sample code for the re-motion mixins tutorial.
// This sample code illustrates the matter discussed
// on the wiki-page "An acting lesson for your pet parrot -- working with 'This'",
// (the alternative with the interface).

using System;
using System.Threading;
using Remotion.Implementation;
using Remotion.Mixins;
using Remotion.Reflection;

// Demonstrate the use of the generic class 'Mixin<TThis>',
// with an interface for 'TThis'. The mixin class 'OnThePhoneMixin'
// uses the 'Say' method of the target class. 

namespace WikiSamples.ParrotMixinInterface
{
  // This is the interface for the target class,
  // to be passed as 'TThis' parameter to 'Mixin<TThis>'
  public interface IParrot
  {
    void Fly ();
    void Whistle ();
    void Say (string s);
  }

  // target class
  // NOTE THAT 'Parrot' is NOT declared to implement 'IParrot' 
  // It would not hurt, but is not required for 'Mixin<IParrot>' 
  // to work
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

  // INTERFACE of the mixin class
  public interface IOnThePhoneMixin
  {
    void PretendToTalkOnThePhone ();
    void Wait (int milliseconds);
  }

  // mixin class, derived from generic 'Mixin<TThis>'
  [Extends (typeof (Parrot))]  // specifies target class

  // 'IParrot' gives 'OnThePhoneMixin' a handle on 'Say'
  // via 'This'. 
  public class OnThePhoneMixin : Mixin<IParrot>, IOnThePhoneMixin
  {
    public void PretendToTalkOnThePhone ()
    {
      This.Say ("Ring ring");
      This.Say ("Ring ring");
      This.Say ("Halloho?");
      Wait (1500);
      This.Say ("Oh! Hi!");
      Wait (3000);
      This.Say ("I'm fine! And yourself?");
      Wait (1500);
      This.Say ("Glad to hear that! I love you! Bye!");
    }

    public void Wait (int milliseconds)
    {
      Thread.Sleep (milliseconds);
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

      var myPetParrot = ObjectFactory.Create<Parrot> (ParamList.Empty);
      Console.WriteLine ("Parrot:");
      myPetParrot.Whistle ();                                       // do as parrots do
      ((IOnThePhoneMixin) myPetParrot).PretendToTalkOnThePhone ();  // show off what you have learned
      Console.WriteLine ();

      Console.ReadLine ();
    }
  }
}
