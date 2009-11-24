// Sample code for the re-motion mixins tutorial.
// This sample code illustrates the matter discussed
// on the wiki-page " re-motion mixins basics -- duck typing"

using System;
using System.Threading;
using Remotion.Implementation;
using Remotion.Mixins;
using Remotion.Reflection;

namespace WikiSamples.ParrotDuckTyping
{
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

  public class Person
  {
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public virtual void Say (string s)
    {
      // an artificially awkward implementation
      // to show that this 'Say' is TOTALLY unlike 
      // 'Parrot's 'Say'
      Console.Write ('"');
      foreach (var c in s)
      {
        Console.Write (c);
      }
      Console.WriteLine ("\"");
    }
  }

  // This interface is ONLY used as parameter for the Mixin<TThis> (below).
  // Note that neither 'Parrot' nor 'Person' implement this interface
  // The magic of connecting interface and implementations is done
  // by the object factory eventually, by name and signature of the
  // invoked method
  public interface ISpeaker
  {
    void Say (string s);
  }

  // INTERFACE of the mixin class
  public interface IOnThePhoneMixin
  {
    void PretendToTalkOnThePhone ();
    void Wait (int milliseconds);
  }

  [Extends (typeof (Parrot))]
  [Extends (typeof (Person))]
  public class OnThePhoneMixin : Mixin<ISpeaker>, IOnThePhoneMixin
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

      // instantiate parrot thespian
      var myPetParrot = ObjectFactory.Create<Parrot> (ParamList.Empty);
      Console.WriteLine ("Parrot:");
      ((IOnThePhoneMixin) myPetParrot).PretendToTalkOnThePhone ();

      Console.WriteLine ();

      // instantiate person thespian
      var myPetPerson = ObjectFactory.Create<Person> (ParamList.Empty);
      Console.WriteLine ("Person");
      ((IOnThePhoneMixin) myPetPerson).PretendToTalkOnThePhone ();

      Console.ReadLine ();
    }
  }
}
