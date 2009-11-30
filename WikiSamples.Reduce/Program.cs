using System;
using System.Collections;
using System.Collections.Generic;
using Remotion.Mixins;
using Remotion.Reflection;

namespace WikiSamples.Reduce
{
  public delegate object ReduceDelegate (object firstItem, object secondItem);

  public interface IReduceMixin
  {
    object InitialValue { get; set; }
    ReduceDelegate ReduceFunction { get; set; }
    object Reduce ();
  }

  [Extends (typeof (IEnumerable))]
  public class ReduceMixin : Mixin <IEnumerable>, IReduceMixin
  {
    public object InitialValue { get; set; }
    public ReduceDelegate ReduceFunction { get; set; }
    public object Reduce ()
    {
      object accumulator = InitialValue;
      foreach (object item in This)
      {
        accumulator = ReduceFunction (accumulator, item);
      }
      return accumulator;
    }
  }

  class Program
  {
    static void Main (string[] args)
    {
      var t = typeof (IMixinTarget);

      var intList = ObjectFactory.Create <List <int>> (ParamList.Empty);
      ((IReduceMixin) intList).InitialValue = 0;
      ((IReduceMixin) intList).ReduceFunction = (object firstItem, object secondItem) => (int) firstItem + (int) secondItem;
      
      intList.Add (1);
      intList.Add (2);
      intList.Add (3);
      intList.Add (4);

      Console.WriteLine (((IReduceMixin) intList).Reduce ());

      var stringList = ObjectFactory.Create <List <string>> (ParamList.Empty);
      ((IReduceMixin) stringList).InitialValue = "";
      ((IReduceMixin) stringList).ReduceFunction = (object firstItem, object secondItem) => (string) firstItem + " " + (string) secondItem;

      stringList.Add ("mocca");
      stringList.Add ("smokes");
      stringList.Add ("reddit");
      stringList.Add ("diet-coke");

      Console.WriteLine (((IReduceMixin) stringList).Reduce ());

      Console.ReadLine ();
    }
  }
}
