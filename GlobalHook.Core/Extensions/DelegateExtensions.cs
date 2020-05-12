using System;

namespace GlobalHook.Core.Extensions
{
    internal static class DelegateExtensions
    {
        /// <summary>
        /// Creates a copy of the delegate of the specified type.
        /// </summary>
        /// <typeparam name="TDelegate">New delegate type.</typeparam>
        /// <param name="action">Delegate to cast.</param>
        /// <returns>Copy of the <paramref name="action"/> of the specified type</returns>
        /// <remarks>
        /// It's a very very very evil method, but I really have no choice, sorry.
        /// </remarks>
        /// <remarks>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.delegate.equals?view=netcore-3.1
        /// Delegates are equal if they are of the same type and share the same targets, methods, and invocation list.
        /// So there's no problem in such code:
        /// <![CDATA[
        /// MyEvent += action.Cast<MyEventType>();
        /// MyEvent -= action.Cast<MyEventType>();
        /// ]]>
        /// </remarks>
        public static TDelegate Cast<TDelegate>(this Delegate action) where TDelegate : Delegate
        {
            _ = action ?? throw new ArgumentNullException(nameof(action));

            // `copy` and `action` have different references.
            object copy = action.Clone();

            do
            {
                // https://gist.github.com/Kir-Antipov/89f2ecb9d0e2a43cffe5f2f615882381
                unsafe
                {
                    // Let's get reference of `copy`
                    TypedReference typedReference = __makeref(copy);
                    IntPtr* pointer = (IntPtr*)(**(IntPtr**)&typedReference).ToPointer();

                    // Object's reference leads us to a pointer to its type's method table.
                    // So... Just replace it with the right one ¯\_(ツ)_/¯
                    pointer[0] = typeof(TDelegate).TypeHandle.Value;
                }
            }
            // There is a very small possibility that the GC will move the object exactly before we change its type,
            // so `copy` will remain the old type.
            while (!(copy is TDelegate));

            // Now `copy` has the type that we need.
            return (TDelegate)copy;
        }
    }
}
