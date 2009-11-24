// Sample code for the re-motion mixins tutorial.
// This sample code illustrates the matter discussed
// on the wiki-page "An ACTING LESSON for your pet parrot -- working with 'Base'"

using System;
using System.Threading;
using Remotion.Mixins;
using Remotion.Reflection;

namespace WikiSamples.ParrotMixinBase
{
  // Target class
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

  // makes the parrot an actor
  public interface IOnThePhoneMixin
  {
    void PretendToTalkOnThePhone ();
    void Wait (int milliseconds);
  }

  // This interface is REQUIRED here, because the TBase
  // parameter for Mixin<TThis, TBase> can ONLY
  // be an interface. (TThis can be both an interface
  // or a class name.)
  public interface IParrot
  {
    void Say (string s);
  }

  // The mixin class not only extends the 'Parrot' class,
  // is also overrides the 'Say' method. HOWEVER, by virtue
  // of passing a 'TBase' parameter ('IParrot'), we now can
  // invoke 'Parrot's 'Say' method (see below). 
  [Extends (typeof (Parrot))]

  /* Alternative:
  public class OnThePhoneMixin : Mixin<IParrot, IParrot>, IOnThePhoneMixin
  */
  public class OnThePhoneMixin : Mixin<Parrot, IParrot>, IOnThePhoneMixin
  {
    [OverrideTarget]
    public void Say (string s)
    {
      Base.Say (s.ToUpper ());
    }

    public void PretendToTalkOnThePhone ()
    {
      Say ("Ring ring"); // overriden ('OnThePhone's) 'Say', synonymous with 'This.Say' here
      Say ("Ring ring"); // ditto
      Say ("Halloho?");  // ditto
      Wait (1500);
      This.Say ("Oh! Hi!");
      Wait (3000);
      Base.Say ("I'm fine! And yourself?"); // BASE ('Parrot's) 'Say' method, courtesy to passing 'IParrot' as 'TBase' param
      Wait (1500);
      Base.Say ("Glad to hear that! I love you! Bye!"); // ditto
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
      var t = typeof (IMixinTarget);

      Console.WriteLine ("Parrot:");
      var myPetParrot = ObjectFactory.Create<Parrot> (ParamList.Empty);
      ((IOnThePhoneMixin) myPetParrot).PretendToTalkOnThePhone ();
      myPetParrot.Whistle ();

      Console.ReadLine ();
    }
  }
}
