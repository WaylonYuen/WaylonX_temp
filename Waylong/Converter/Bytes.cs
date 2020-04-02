using System;

namespace Waylong.Converter {

    public static class Bytes {

        /// <summary>
        /// 提取內容：提取指定位置內容
        /// </summary>
        /// <param name="source_Bytes">被提取的Bytes</param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// /// <param name="InfoStr"></param>
        /// <returns></returns>
        public static byte[] Extract(byte[] source_Bytes, int length) {

            try {
                var dataResult_Bytes = new byte[length];
                Array.Copy(source_Bytes, 0, dataResult_Bytes, 0, length);
                return dataResult_Bytes;

            } catch (Exception e) {
                //Display.Format(State.Catch, "提取內容錯誤", $"Extract() >> {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 提取內容：提取指定位置內容
        /// </summary>
        /// <param name="source_Bytes">被提取的Bytes</param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// /// <param name="InfoStr"></param>
        /// <returns></returns>
        public static byte[] Extract(byte[] source_Bytes, int startIndex, int length) {

            try {
                var dataResult_Bytes = new byte[length];
                Array.Copy(source_Bytes, startIndex, dataResult_Bytes, 0, length);
                return dataResult_Bytes;

            } catch (Exception e) {
                //Display.Format(State.Catch, "提取內容錯誤", $"Extract() >> {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 提取內容：提取指定位置內容
        /// </summary>
        /// <param name="source_Bytes">被提取的Bytes</param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// /// <param name="InfoStr"></param>
        /// <returns></returns>
        public static byte[] Extract(byte[] source_Bytes, int startIndex, int destinationIndex, int length) {

            try {
                var dataResult_Bytes = new byte[length];
                Array.Copy(source_Bytes, startIndex, dataResult_Bytes, destinationIndex, length);
                return dataResult_Bytes;

            } catch (Exception e) {
                //Display.Format(State.Catch, "提取內容錯誤", $"Extract() >> {e.Message}");
                return null;
            }
        }

    }

}
