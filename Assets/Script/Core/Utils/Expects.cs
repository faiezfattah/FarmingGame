using System;
using UnityEngine;

#nullable enable

namespace Script.Core.Utils {
    public static class Expects {
        public static T Expect<T>(this T? value) {
            if (value is not null) return value;
            Debug.Log($"[Expect] value is null: {value?.GetType().Name ?? "Unknown"}");
            return default!;
        }

        public static T ExpectOrThrow<T>(this T? value, Exception? exception = null) {
            if (value is not null) return value;
            throw exception ?? new Exception($"[Expect] value is null: {typeof(T).Name}");
        }

        public static T ExpectOrHandle<T>(this T? value, Action<T?> handler) {
            if (value is not null) return value;
            handler(value);
            return default!;
        }

        public static T ExpectWithMessage<T>(this T? value, string message) {
            if (value is not null) return value;
            Debug.LogError($"[Expect] {message}");
            return default!;
        }
        public static T ExpectOrDefault<T>(this T? value, T defaultValue) {
            return value ?? defaultValue;
        }
        public static T ExpectOrCreate<T>(this T? value) where T : class, new() {
            return value ?? new T();
        }
        public static T ExpectOrCreate<T>(this T? value, Func<T> factory) {
            return value ?? factory();
        }
        public static T ExpectOrCreate<T>(this T? value) where T : struct {
            return value ?? new T();
        }
    }
}