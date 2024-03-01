using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Foundation {
	public static partial class NativeArrayExtensions {
		public interface IFillJob : IJobParallelFor { }

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		public struct FillJob<T> : IFillJob where T : struct {
			[ReadOnly] public T Value;
			public NativeArray<T> Values;

			public FillJob(NativeArray<T> values, in T value) {
				Value = value;
				Values = values;
			}

			public void Execute(int index) {
				Values[index] = Value;
			}
		}
	}
}