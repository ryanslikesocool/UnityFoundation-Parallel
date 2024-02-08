using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

namespace Foundation.Parallel {
	public static partial class NativeArrayExtensions {
		// MARK: - Toggle

		/// <remarks>
		/// Assumes input.Length == output.Length
		/// </remarks>
		public static JobHandle Toggle(NativeArray<bool> input, NativeArray<bool> output, bool deallocateInput = false, int innerloopBatchCount = 128, JobHandle dependency = default) {
			if (deallocateInput) {
				return new ToggleJob_DeallocateInput {
					Input = input,
					Output = output
				}.Schedule(input.Length, innerloopBatchCount, dependency);
			} else {
				return new ToggleJob {
					Input = input,
					Output = output
				}.Schedule(input.Length, innerloopBatchCount, dependency);
			}
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct ToggleJob : IJobParallelFor {
			[ReadOnly] public NativeArray<bool> Input;
			[WriteOnly] public NativeArray<bool> Output;

			public void Execute(int index) {
				Output[index] = !Input[index];
			}
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct ToggleJob_DeallocateInput : IJobParallelFor {
			[ReadOnly, DeallocateOnJobCompletion] public NativeArray<bool> Input;
			[WriteOnly] public NativeArray<bool> Output;

			public void Execute(int index) {
				Output[index] = !Input[index];
			}
		}

		// MARK: - Toggle In Place

		/// <remarks>
		/// Assumes input.Length == output.Length
		/// </remarks>
		public static JobHandle ToggleInPlace(NativeArray<bool> values, int innerloopBatchCount = 128, JobHandle dependency = default) => new ToggleInPlaceJob {
			Values = values
		}.Schedule(values.Length, innerloopBatchCount, dependency);

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct ToggleInPlaceJob : IJobParallelFor {
			public NativeArray<bool> Values;

			public void Execute(int index) {
				Values[index] = !Values[index];
			}
		}
	}
}