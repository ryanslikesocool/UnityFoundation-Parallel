

using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Foundation.Parallel {
	public static partial class NativeArrayExtensions {
		/// <remarks>
		/// Assumes input.Length == output.Length
		/// </remarks>
		public static JobHandle Reverse<T>(NativeArray<T> input, NativeArray<T> output, bool deallocateInput = false, int innerloopBatchCount = 128, JobHandle dependency = default) where T : struct {
			if (deallocateInput) {
				return new ReverseJob_DeallocateInput<T> {
					Length = input.Length,
					Input = input,
					Output = output
				}.Schedule(input.Length, innerloopBatchCount, dependency);
			} else {
				return new ReverseJob<T> {
					Length = input.Length,
					Input = input,
					Output = output
				}.Schedule(input.Length, innerloopBatchCount, dependency);
			}
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct ReverseJob<T> : IJobParallelFor where T : struct {
			[ReadOnly] public int Length;
			[ReadOnly, NativeDisableParallelForRestriction] public NativeArray<T> Input;
			[WriteOnly] public NativeArray<T> Output;

			public void Execute(int index) {
				Output[index] = Input[Length - (index + 1)];
			}
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct ReverseJob_DeallocateInput<T> : IJobParallelFor where T : struct {
			[ReadOnly] public int Length;
			[ReadOnly, NativeDisableParallelForRestriction, DeallocateOnJobCompletion] public NativeArray<T> Input;
			[WriteOnly] public NativeArray<T> Output;

			public void Execute(int index) {
				Output[index] = Input[Length - (index + 1)];
			}
		}
	}
}