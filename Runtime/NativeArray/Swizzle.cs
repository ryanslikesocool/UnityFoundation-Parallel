#if UNITY_MATHEMATICS
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Foundation.Parallel {
	public static partial class NativeArrayExtensions {
		public interface ISwizzleJob : IJobParallelFor { }

		// MARK: - float2

		public static JobHandle Swizzle(NativeArray<float2> input, NativeArray<float2> output, in int2 mask, bool deallocateInput = false, int innerloopBatchCount = 128, JobHandle dependency = default) {
			if (deallocateInput) {
				return new SwizzleJob_Float2_DeallocateInput {
					Mask = mask,
					Input = input,
					Output = output
				}.Schedule(input.Length, innerloopBatchCount, dependency);
			} else {
				return new SwizzleJob_Float2 {
					Mask = mask,
					Input = input,
					Output = output
				}.Schedule(input.Length, innerloopBatchCount, dependency);
			}
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct SwizzleJob_Float2 : ISwizzleJob {
			[ReadOnly] public int2 Mask;
			[ReadOnly] public NativeArray<float2> Input;
			[WriteOnly] public NativeArray<float2> Output;

			public void Execute(int index) {
				float2 output = float2.zero;

				for (int lane = 0; lane < 2; lane++) {
					output[lane] = Input[index][lane];
				}

				Output[index] = output;
			}
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct SwizzleJob_Float2_DeallocateInput : ISwizzleJob {
			[ReadOnly] public int2 Mask;
			[ReadOnly, DeallocateOnJobCompletion] public NativeArray<float2> Input;
			[WriteOnly] public NativeArray<float2> Output;

			public void Execute(int index) {
				float2 output = float2.zero;

				for (int lane = 0; lane < 2; lane++) {
					output[lane] = Input[index][lane];
				}

				Output[index] = output;
			}
		}

		// MARK: In Place

		public static JobHandle SwizzleInPlace(NativeArray<float2> values, in int2 mask, int innerloopBatchCount = 128, JobHandle dependency = default) => new SwizzleJob_Float2_InPlace {
			Mask = mask,
			Values = values
		}.Schedule(values.Length, innerloopBatchCount, dependency);

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct SwizzleJob_Float2_InPlace : ISwizzleJob {
			[ReadOnly] public int2 Mask;
			public NativeArray<float2> Values;

			public void Execute(int index) {
				float2 output = float2.zero;

				for (int lane = 0; lane < 2; lane++) {
					output[lane] = Values[index][lane];
				}

				Values[index] = output;
			}
		}

		// MARK: - float3

		public static JobHandle Swizzle(NativeArray<float3> input, NativeArray<float3> output, in int3 mask, bool deallocateInput = false, int innerloopBatchCount = 128, JobHandle dependency = default) {
			if (deallocateInput) {
				return new SwizzleJob_Float3_DeallocateInput {
					Mask = mask,
					Input = input,
					Output = output
				}.Schedule(input.Length, innerloopBatchCount, dependency);
			} else {
				return new SwizzleJob_Float3 {
					Mask = mask,
					Input = input,
					Output = output
				}.Schedule(input.Length, innerloopBatchCount, dependency);
			}
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct SwizzleJob_Float3 : ISwizzleJob {
			[ReadOnly] public int3 Mask;
			[ReadOnly] public NativeArray<float3> Input;
			[WriteOnly] public NativeArray<float3> Output;

			public void Execute(int index) {
				float3 output = float3.zero;

				for (int lane = 0; lane < 3; lane++) {
					output[lane] = Input[index][lane];
				}

				Output[index] = output;
			}
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct SwizzleJob_Float3_DeallocateInput : ISwizzleJob {
			[ReadOnly] public int3 Mask;
			[ReadOnly, DeallocateOnJobCompletion] public NativeArray<float3> Input;
			[WriteOnly] public NativeArray<float3> Output;

			public void Execute(int index) {
				float3 output = float3.zero;

				for (int lane = 0; lane < 3; lane++) {
					output[lane] = Input[index][lane];
				}

				Output[index] = output;
			}
		}

		// MARK:  In Place

		public static JobHandle SwizzleInPlace(NativeArray<float3> values, in int3 mask, int innerloopBatchCount = 128, JobHandle dependency = default) => new SwizzleJob_Float3_InPlace {
			Mask = mask,
			Values = values
		}.Schedule(values.Length, innerloopBatchCount, dependency);

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct SwizzleJob_Float3_InPlace : ISwizzleJob {
			[ReadOnly] public int3 Mask;
			public NativeArray<float3> Values;

			public void Execute(int index) {
				float3 output = float3.zero;

				for (int lane = 0; lane < 3; lane++) {
					output[lane] = Values[index][lane];
				}

				Values[index] = output;
			}
		}

		// MARK: - float4

		public static JobHandle Swizzle(NativeArray<float4> input, NativeArray<float4> output, in int4 mask, bool deallocateInput = false, int innerloopBatchCount = 128, JobHandle dependency = default) {
			if (deallocateInput) {
				return new SwizzleJob_Float4_DeallocateInput {
					Mask = mask,
					Input = input,
					Output = output
				}.Schedule(input.Length, innerloopBatchCount, dependency);
			} else {
				return new SwizzleJob_Float4 {
					Mask = mask,
					Input = input,
					Output = output
				}.Schedule(input.Length, innerloopBatchCount, dependency);
			}
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct SwizzleJob_Float4 : ISwizzleJob {
			[ReadOnly] public int4 Mask;
			[ReadOnly] public NativeArray<float4> Input;
			[WriteOnly] public NativeArray<float4> Output;

			public void Execute(int index) {
				float4 output = float4.zero;

				for (int lane = 0; lane < 4; lane++) {
					output[lane] = Input[index][lane];
				}

				Output[index] = output;
			}
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct SwizzleJob_Float4_DeallocateInput : ISwizzleJob {
			[ReadOnly] public int4 Mask;
			[ReadOnly, DeallocateOnJobCompletion] public NativeArray<float4> Input;
			[WriteOnly] public NativeArray<float4> Output;

			public void Execute(int index) {
				float4 output = float4.zero;

				for (int lane = 0; lane < 4; lane++) {
					output[lane] = Input[index][lane];
				}

				Output[index] = output;
			}
		}

		// MARK:  In Place

		public static JobHandle SwizzleInPlace(NativeArray<float4> values, in int4 mask, int innerloopBatchCount = 128, JobHandle dependency = default) => new SwizzleJob_Float4_InPlace {
			Mask = mask,
			Values = values
		}.Schedule(values.Length, innerloopBatchCount, dependency);

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
		private struct SwizzleJob_Float4_InPlace : ISwizzleJob {
			[ReadOnly] public int4 Mask;
			public NativeArray<float4> Values;

			public void Execute(int index) {
				float4 output = float4.zero;

				for (int lane = 0; lane < 4; lane++) {
					output[lane] = Values[index][lane];
				}

				Values[index] = output;
			}
		}
	}
}
#endif