using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Foundation {
	public static partial class NativeArrayExtensions {
		public interface IReverseJob : IJobParallelFor { }

		/// <remarks>
		/// Assumes input.Length == output.Length
		/// </remarks>
		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		public struct ReverseJob<T> : IReverseJob where T : struct {
			[ReadOnly] public int Length;
			[ReadOnly, NativeDisableParallelForRestriction] public NativeArray<T> Input;
			[WriteOnly] public NativeArray<T> Output;

			public ReverseJob(NativeArray<T> input, NativeArray<T> output, int length) {
				Length = length;
				Input = input;
				Output = output;
			}

			public void Execute(int index) {
				Output[index] = Input[Length - (index + 1)];
			}
		}

		/// <remarks>
		/// Assumes input.Length == output.Length
		/// </remarks>
		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		public struct ReverseJob_DeallocateInput<T> : IReverseJob where T : struct {
			[ReadOnly] public int Length;
			[ReadOnly, NativeDisableParallelForRestriction, DeallocateOnJobCompletion] public NativeArray<T> Input;
			[WriteOnly] public NativeArray<T> Output;

			public ReverseJob_DeallocateInput(NativeArray<T> input, NativeArray<T> output, int length) {
				Length = length;
				Input = input;
				Output = output;
			}

			public void Execute(int index) {
				Output[index] = Input[Length - (index + 1)];
			}
		}
	}
}