using System;
using System.Runtime.InteropServices;

namespace liwq.CLib
{
    #region CStdlib
    public static class CStdlib
    {
        public const int BYTE_SIZE = 1;
        public const int SBYTE_SIZE = 1;
        public const int SHORT_SIZE = 2;
        public const int USHORT_SIZE = 2;
        public const int INT_SIZE = 4;
        public const int UINT_SIZE = 4;
        public const int LONG_SIZE = 8;
        public const int ULONG_SIZE = 8;
        public const int FLOAT_SIZE = 4;
        public const int DOUBLE_SIZE = 8;

        unsafe public static void* malloc(int size)
        {
            void* p = Marshal.AllocHGlobal(size + 4).ToPointer();
            ((int*)p)[0] = size;
            return (void*)(((int)p) + 4);
        }
        unsafe public static void free(void* memblock)
        {
            Marshal.FreeHGlobal((IntPtr)((int)memblock - 4));
        }
        unsafe public static void* realloc(void* memblock, int newSize)
        {
            void* newMemblock = malloc(newSize);
            int oldSize = ((int*)((int)memblock - 4))[0];
            CString.memcpy(newMemblock, memblock, (uint)oldSize);
            free(memblock);
            return newMemblock;
        }
    }
    #endregion //CStdlib

    #region CString
    public static class CString
    {
        /////////////////////////////////////////////////

        #region FASTEST

        unsafe public static void _memcpyi4(void* dest, void* src)
        {
            ((int*)dest)[0] = ((int*)src)[0];
        }
        unsafe public static void _memcpyi8(void* dest, void* src)
        {
            ((int*)dest)[0] = ((int*)src)[0];
            ((int*)dest)[1] = ((int*)src)[1];
        }
        unsafe public static void _memcpyi16(void* dest, void* src)
        {
            ((int*)dest)[0] = ((int*)src)[0];
            ((int*)dest)[1] = ((int*)src)[1];
            ((int*)dest)[2] = ((int*)src)[2];
            ((int*)dest)[3] = ((int*)src)[3];
        }
        unsafe public static void _memcpyi32(void* dest, void* src)
        {
            _memcpyi16(dest, src);
            _memcpyi16((void*)((int)dest + 16), (void*)((int)src + 16));
        }
        unsafe public static void _memcpyi64(void* dest, void* src)
        {
            _memcpyi16(dest, src);
            _memcpyi16((void*)((int)dest + 16), (void*)((int)src + 16));
            _memcpyi16((void*)((int)dest + 32), (void*)((int)src + 32));
            _memcpyi16((void*)((int)dest + 48), (void*)((int)src + 48));
        }
        unsafe public static void _memcpyi128(void* dest, void* src)
        {
            _memcpyi16(dest, src);
            _memcpyi16((void*)((int)dest + 16), (void*)((int)src + 16));
            _memcpyi16((void*)((int)dest + 32), (void*)((int)src + 32));
            _memcpyi16((void*)((int)dest + 48), (void*)((int)src + 48));
            _memcpyi16((void*)((int)dest + 64), (void*)((int)src + 64));
            _memcpyi16((void*)((int)dest + 80), (void*)((int)src + 80));
            _memcpyi16((void*)((int)dest + 96), (void*)((int)src + 96));
            _memcpyi16((void*)((int)dest + 112), (void*)((int)src + 112));
        }

        /////////////////////////////////////////////////

        unsafe public static void _memcpys4(void* dest, void* src)
        {
            ((short*)dest)[0] = ((short*)src)[0];
            ((short*)dest)[1] = ((short*)src)[1];
        }
        unsafe public static void _memcpys8(void* dest, void* src)
        {
            ((short*)dest)[0] = ((short*)src)[0];
            ((short*)dest)[1] = ((short*)src)[1];
            ((short*)dest)[2] = ((short*)src)[2];
            ((short*)dest)[3] = ((short*)src)[3];
        }
        unsafe public static void _memcpys16(void* dest, void* src)
        {
            _memcpys8(dest, src);
            _memcpys8((void*)((int)dest + 8), (void*)((int)src + 8));
        }
        unsafe public static void _memcpys32(void* dest, void* src)
        {
            _memcpys8(dest, src);
            _memcpys8((void*)((int)dest + 8), (void*)((int)src + 8));
            _memcpys8((void*)((int)dest + 16), (void*)((int)src + 16));
            _memcpys8((void*)((int)dest + 24), (void*)((int)src + 24));
        }
        unsafe public static void _memcpys64(void* dest, void* src)
        {
            _memcpys8(dest, src);
            _memcpys8((void*)((int)dest + 8), (void*)((int)src + 8));
            _memcpys8((void*)((int)dest + 16), (void*)((int)src + 16));
            _memcpys8((void*)((int)dest + 24), (void*)((int)src + 24));
            _memcpys8((void*)((int)dest + 32), (void*)((int)src + 32));
            _memcpys8((void*)((int)dest + 40), (void*)((int)src + 40));
            _memcpys8((void*)((int)dest + 48), (void*)((int)src + 48));
            _memcpys8((void*)((int)dest + 56), (void*)((int)src + 56));
        }
        unsafe public static void _memcpys128(void* dest, void* src)
        {
            for (int i = 0; i < 16; i++)
            {
                _memcpyi16(dest, src);
                dest = (void*)((int)dest + 8);
                src = (void*)((int)src + 8);
            }
        }

        /////////////////////////////////////////////////

        unsafe public static void _memcpyb4(void* dest, void* src)
        {
            ((byte*)dest)[0] = ((byte*)src)[0];
            ((byte*)dest)[1] = ((byte*)src)[1];
            ((byte*)dest)[2] = ((byte*)src)[2];
            ((byte*)dest)[3] = ((byte*)src)[3];
        }
        unsafe public static void _memcpyb8(void* dest, void* src)
        {
            ((byte*)dest)[0] = ((byte*)src)[0];
            ((byte*)dest)[1] = ((byte*)src)[1];
            ((byte*)dest)[2] = ((byte*)src)[2];
            ((byte*)dest)[3] = ((byte*)src)[3];
            ((byte*)dest)[4] = ((byte*)src)[4];
            ((byte*)dest)[5] = ((byte*)src)[5];
            ((byte*)dest)[6] = ((byte*)src)[6];
            ((byte*)dest)[7] = ((byte*)src)[7];
        }

        /// <param name="c">注意是4字节</param>
        unsafe public static void _memseti4(void* dest, int c)
        {
            ((int*)dest)[0] = c;
        }
        /// <param name="c">注意是4字节</param>
        unsafe public static void _memseti8(void* dest, int c)
        {
            ((int*)dest)[0] = c;
            ((int*)dest)[1] = c;
        }
        /// <param name="c">注意是4字节</param>
        unsafe public static void _memseti16(void* dest, int c)
        {
            ((int*)dest)[0] = c;
            ((int*)dest)[1] = c;
            ((int*)dest)[2] = c;
            ((int*)dest)[3] = c;
        }
        /// <param name="c">注意是4字节</param>
        unsafe public static void _memseti32(void* dest, int c)
        {
            _memseti16(dest, c);
            _memseti16((void*)((int)dest + 16), c);
        }
        /// <param name="c">注意是4字节</param>
        unsafe public static void _memseti64(void* dest, int c)
        {
            _memseti16(dest, c);
            _memseti16((void*)((int)dest + 16), c);
            _memseti16((void*)((int)dest + 32), c);
            _memseti16((void*)((int)dest + 48), c);
        }
        /// <param name="c">注意是4字节</param>
        unsafe public static void _memseti128(void* dest, int c)
        {
            _memseti16(dest, c);
            _memseti16((void*)((int)dest + 16), c);
            _memseti16((void*)((int)dest + 32), c);
            _memseti16((void*)((int)dest + 48), c);
            _memseti16((void*)((int)dest + 64), c);
            _memseti16((void*)((int)dest + 80), c);
            _memseti16((void*)((int)dest + 96), c);
            _memseti16((void*)((int)dest + 112), c);
        }

        #endregion FASTEST

        /////////////////////////////////////////////////

        /// <summary>1小块的多少字节</summary>
        const uint LITTLE_BLOCK_SIZE = CStdlib.INT_SIZE;
        /// <summary>1大块多少字节</summary>
        const uint BIG_BLOCK_SIZE = CStdlib.INT_SIZE * 4;

        public static int MAKE_MASK(int c)
        {
            c |= (c << 8);
            return (c |= (c << 16));
        }

        /////////////////////////////////////////////////

        #region memchr

        /// <summary>Finds characters in a buffer.</summary>
        /// <param name="buf">Pointer to buffer.</param>
        /// <param name="c">Character to look for.</param>
        /// <param name="count">Number of characters to check.</param>
        /// <returns>If successful, memchr returns a pointer to the first location of c in buf; otherwise, it returns NULL.</returns>
        /// <remarks>The memchr function looks for the first occurrence of c in the first count bytes of buf.
        /// It stops when it finds c or when it has checked the first count bytes.</remarks>
        unsafe static public void* memchr(void* buf, int c, uint count)
        {
            c &= 0xFF;
            if ((count >= LITTLE_BLOCK_SIZE) && (((uint)buf & (LITTLE_BLOCK_SIZE - 1)) == 0))
            {
                uint mask = (uint)MAKE_MASK(c);
                while (count >= LITTLE_BLOCK_SIZE)
                {
                    uint buffer = ((uint*)buf)[0] ^ mask;
                    if (((buffer - 0x01010101) & ~buffer & 0x80808080) != 0)
                    {
                        if (((byte*)buf)[0] == c) return (void*)((int)buf + 0);
                        if (((byte*)buf)[1] == c) return (void*)((int)buf + 1);
                        if (((byte*)buf)[2] == c) return (void*)((int)buf + 2);
                        if (((byte*)buf)[3] == c) return (void*)((int)buf + 3);
                    }
                    buf = (void*)((int)buf + 4);
                    count -= LITTLE_BLOCK_SIZE;
                }
            }
            if (count != 0)
            {
                do
                {
                    if (((byte*)buf)[0] == c) return buf;
                    buf = (void*)((int)buf + 1);
                    count--;
                } while (count != 0);
            }
            return null;
        }

        #endregion memchr

        #region memcmp

        /// <summary>Compare characters in two buffers.</summary>
        /// <param name="buf1">First buffer.</param>
        /// <param name="buf2">Second buffer.</param>
        /// <param name="count">Number of characters.</param>
        /// <returns>
        /// The return value indicates the relationship between the buffers.
        /// < 0 buf1 less than buf2 
        /// 0 buf1 identical to buf2 
        /// > 0 buf1 greater than buf2 
        /// </returns>
        /// <remarks>The memcmp function compares the first count bytes of buf1 and buf2 and returns a value indicating their relationship.</remarks>
        unsafe static public int memcmp(void* buf1, void* buf2, uint count)
        {
            if ((count >= LITTLE_BLOCK_SIZE) && ((((uint)buf1 | (uint)buf1) & (LITTLE_BLOCK_SIZE - 1)) == 0))
            {
                do
                {
                    if (((uint*)buf1)[0] != ((uint*)buf2)[0])
                        break;
                    buf1 = (void*)((int)buf1 + 4);
                    buf2 = (void*)((int)buf2 + 4);
                    count -= LITTLE_BLOCK_SIZE;
                }
                while (count >= LITTLE_BLOCK_SIZE);
            }
            if (count != 0)
            {
                do
                {
                    --count;
                    if (((byte*)buf1)[0] != ((byte*)buf2)[0])
                        return ((byte*)buf1)[0] - ((byte*)buf2)[0];
                    buf1 = (void*)((int)buf1 + 1);
                    buf2 = (void*)((int)buf2 + 1);
                } while (count != 0);
            }
            return 0;
        }

        #endregion memcmp

        #region memcpy

        /// <summary>Copies characters between buffers.</summary>
        /// <param name="dest">New buffer.</param>
        /// <param name="src">Buffer to copy from.</param>
        /// <param name="count">Number of characters to copy.</param>
        /// <returns>memcpy returns the value of dest.</returns>
        /// <remarks>The memcpy function copies count characters of src to dest.
        /// If the source and destination overlap, 
        /// this function does not ensure that the original source characters in the overlapping region are copied before being overwritten. 
        /// Use memmove to handle overlapping regions.
        /// </remarks>
        unsafe public static void* memcpy(void* dest, void* src, uint count)
        {
            int s = ((int)dest | (int)src) & 3;
            if (s == 0)
                return _memcpyInt(dest, src, count);
            else if (s == 2)
                return _memcpyShort(dest, src, count);
            else
                return _memcpyByte(dest, src, count);
        }
        unsafe public static void* _memcpyInt(void* dest, void* src, uint count)
        {
            void* ret = dest;
            while (count >= 16)
            {
                ((int*)dest)[0] = ((int*)src)[0];
                ((int*)dest)[1] = ((int*)src)[1];
                ((int*)dest)[2] = ((int*)src)[2];
                ((int*)dest)[3] = ((int*)src)[3];
                dest = (void*)((int)dest + 16);
                src = (void*)((int)src + 16);
                count -= 16;
            }
            while (count >= 4)
            {
                ((int*)dest)[0] = ((int*)src)[0];
                dest = (void*)((int)dest + 4);
                src = (void*)((int)src + 4);
                count -= 4;
            }
            while (count-- != 0)
            {
                ((byte*)dest)[0] = ((byte*)src)[0];
                dest = (void*)((int)dest + 1);
                src = (void*)((int)src + 1);
            }
            return ret;
        }
        unsafe public static void* _memcpyShort(void* dest, void* src, uint count)
        {
            void* ret = dest;
            while (count >= 8)
            {
                ((short*)dest)[0] = ((short*)src)[0];
                ((short*)dest)[1] = ((short*)src)[1];
                ((short*)dest)[2] = ((short*)src)[2];
                ((short*)dest)[3] = ((short*)src)[3];
                dest = (void*)((int)dest + 8);
                src = (void*)((int)src + 8);
                count -= 8;
            }
            while (count >= 4)
            {
                ((short*)dest)[0] = ((short*)src)[0];
                ((short*)dest)[1] = ((short*)src)[1];
                dest = (void*)((int)dest + 4);
                src = (void*)((int)src + 4);
                count -= 4;
            }
            while (count-- != 0)
            {
                ((byte*)dest)[0] = ((byte*)src)[0];
                dest = (void*)((int)dest + 1);
                src = (void*)((int)src + 1);
            }
            return ret;
        }
        unsafe public static void* _memcpyByte(void* dest, void* src, uint count)
        {
            void* ret = dest;
            while (count >= 4)
            {
                ((byte*)dest)[0] = ((byte*)src)[0];
                ((byte*)dest)[1] = ((byte*)src)[1];
                ((byte*)dest)[2] = ((byte*)src)[2];
                ((byte*)dest)[3] = ((byte*)src)[3];
                dest = (void*)((int)dest + 4);
                src = (void*)((int)src + 4);
                count -= 4;
            }
            while (count-- != 0)
            {
                ((byte*)dest)[0] = ((byte*)src)[0];
                dest = (void*)((int)dest + 1);
                src = (void*)((int)src + 1);
            }
            return ret;
        }

        #endregion memcpy

        #region memmove

        /// <summary>Moves one buffer to another.</summary>
        /// <param name="dest">Destination object.</param>
        /// <param name="src">Source object.</param>
        /// <param name="count">Number of characters to copy.</param>
        /// <returns>memmove returns the value of dest.</returns>
        /// <remarks>
        /// The memmove function copies count characters from src to dest. 
        /// If some regions of the source area and the destination overlap,
        /// memmove ensures that the original source characters in the overlapping region are copied before being overwritten.
        /// </remarks>
        unsafe public static void* memmove(void* dest, void* src, uint count)
        {
            /// Destructive overlap...have to copy backwards
            if (((uint)src < (uint)dest) && ((uint)dest < (uint)src + count))
            {
                void* ret = dest;
                src = (void*)((uint)src + count);
                dest = (void*)((uint)dest + count);
                while (count-- != 0)
                {
                    dest = (void*)((uint)dest - 1);
                    src = (void*)((uint)src - 1);
                    ((byte*)dest)[0] = ((byte*)src)[0];
                }
                return ret;
            }
            return memcpy(dest, src, count);
        }

        #endregion memmove

        #region memset

        unsafe public static void* _memset4b(void* dest, int c, uint count)
        {
            void* ret = dest;
            c |= c << 8;
            c |= c << 16;
            while (count >= 16)
            {
                ((int*)dest)[0] = c;
                ((int*)dest)[1] = c;
                ((int*)dest)[2] = c;
                ((int*)dest)[3] = c;
                dest = (void*)((int)dest + 16);
                count -= 16;
            }
            while (count >= 4)
            {
                ((int*)dest)[0] = c;
                dest = (void*)((int)dest + 4);
                count -= 4;
            }
            while (count-- != 0)
            {
                ((byte*)(dest))[0] = (byte)c;
                dest = (void*)((int)dest + 1);
            }
            return ret;
        }

        /// <summary>Sets buffers to a specified character.</summary>
        /// <param name="dest">Pointer to destination.</param>
        /// <param name="c">Character to set.</param>
        /// <param name="count">Number of characters.</param>
        /// <returns>memset and wmemset return the value of dest.</returns>
        /// <remarks>The memset and wmemset functions set the first count characters of dest to the character c. </remarks>
        unsafe public static void* memset(void* dest, int c, uint count)
        {
            void* ret = dest;

            for (int i = (int)dest & 3; i != 0; i++)
            {
                ((byte*)dest)[0] = (byte)c;
                dest = (void*)((int)dest + 1);
                count -= 1;
            }
            c |= c << 8;
            c |= c << 16;
            while (count >= 16)
            {
                ((int*)dest)[0] = c;
                ((int*)dest)[1] = c;
                ((int*)dest)[2] = c;
                ((int*)dest)[3] = c;
                dest = (void*)((int)dest + 16);
                count -= 16;
            }
            while (count >= 4)
            {
                ((int*)dest)[0] = c;
                dest = (void*)((int)dest + 4);
                count -= 4;
            }
            while (count-- != 0)
            {
                ((byte*)(dest))[0] = (byte)c;
                dest = (void*)((int)dest + 1);
            }
            return ret;
        }

        /// <summary>
        /// 把count的uint改成int,方便书写.
        /// 因为申请的内存不会非常大,所以没有转换上出错的问题
        /// </summary>
        unsafe public static void* memset(void* dest, int c, int count)
        {
            return memset(dest, c, (uint)count);
        }

        #endregion memset

        #region strcat

        /// <summary>Append a string.</summary>
        /// <param name="strDestination">Null-terminated destination string.</param>
        /// <param name="strSource">Null-terminated source string.</param>
        /// <returns>Each of these functions returns the destination string (strDestination).
        /// No return value is reserved to indicate an error.</returns>
        /// <remarks>
        /// The strcat function appends strSource to strDestination and terminates the resulting string with a null character.
        /// The initial character of strSource overwrites the terminating null character of strDestination.
        /// No overflow checking is performed when strings are copied or appended.
        /// The behavior of strcat is undefined if the source and destination strings overlap.
        /// </remarks>
        /// <Security>
        /// The first argument, strDestination, must be large enough to hold the current strDestination and strSource combined and a closing '\0';
        /// otherwise, a buffer overrun can occur. 
        /// This can lead to a denial of service attack against the application if an access violation occurs,
        /// or in the worst case, allow an attacker to inject executable code into your process.
        /// This is especially true if strDestination is a stack-based buffer.
        /// Consider using strncat or wcsncat, or consider using an appropriate strsafe function.
        /// </Security>
        unsafe public static sbyte* strcat(sbyte* strDestination, sbyte* strSource)
        {
            sbyte* ret = strDestination;
            //首先跳到空字符位置
            if (((uint)strDestination & (LITTLE_BLOCK_SIZE - 1)) == 0)
            {
                uint v = ((uint*)strDestination)[0];
                if (((v - 0x1010101) & ~v & 0x80808080) == 0)
                {
                    do { strDestination = (sbyte*)((int)strDestination + 4); v = ((uint*)strDestination)[0]; }
                    while (((v - 0x1010101) & ~v & 0x80808080) == 0);
                }
            }
            sbyte srcb = strDestination[0];
            if (srcb != 0)
            {
                do { ++strDestination; srcb = strDestination[0]; }
                while (srcb != 0);
            }
            //可以开始复制了
            strcpy(strDestination, strSource);
            return ret;
        }

        #endregion strcat

        #region strchr

        /// <summary>Find a character in a string.</summary>
        /// <param name="str">Null-terminated source string.</param>
        /// <param name="i">Character to be located.</param>
        /// <returns>Each of these functions returns a pointer to the first occurrence of c in string, or NULL if c is not found.</returns>
        /// <remarks>
        /// The strchr function finds the first occurrence of c in string, or it returns NULL if c is not found.
        /// The null-terminating character is included in the search.
        /// </remarks>
        unsafe public static sbyte* strchr(sbyte* str, int i)
        {
            i &= 0xFF;
            if (((uint)str & (LITTLE_BLOCK_SIZE - 1)) == 0)
            {
                uint mask = (uint)MAKE_MASK(i);
                uint ui = ((uint*)str)[0];
                uint chk = ((ui - 0x1010101) & ~ui & 0x80808080);
                if ((chk == 0) && ((chk ^ mask) == 0))
                {
                    do
                    {
                        str = (sbyte*)((int)str + 4);
                        ui = ((uint*)str)[0];
                        chk = ((ui - 0x1010101) & ~ui & 0x80808080);
                    } while ((chk == 0) && ((chk ^ mask) == 0));
                }
            }
            sbyte b = str[0];
            if (b != 0 && b != i)
            {
                do
                {
                    str++;
                    b = str[0];
                }
                while (b != 0 && b != i);
            }
            if (b == i) return (sbyte*)str;
            return null;
        }

        #endregion strchr

        #region strcmp

        /// <summary>Compare strings.</summary>
        /// <param name="string1">Null-terminated strings to compare.</param>
        /// <param name="string2">Null-terminated strings to compare.</param>
        /// <returns>
        /// The return value for each of these functions indicates the lexicographic relation of string1 to string2.
        /// < 0 string1 less than string2 
        /// 0 string1 identical to string2 
        /// > 0 string1 greater than string2
        /// </returns>
        /// <remarks>The strcmp function compares string1 and string2 lexicographically and returns a value indicating their relationship.</remarks>
        unsafe public static int strcmp(sbyte* string1, sbyte* string2)
        {
            uint* a1;
            uint* a2;

            /// If s1 or s2 are unaligned, then compare bytes.
            if ((((uint)string1 | (uint)string2) & (LITTLE_BLOCK_SIZE - 1)) == 0)
            {
                /// If s1 and s2 are word-aligned, compare them a word at a time.
                a1 = (uint*)string1;
                a2 = (uint*)string2;

                while (a1[0] == a2[0])
                {
                    /// To get here, *a1 == *a2, thus if we find a null in *a1, then the strings must be equal, so return zero.
                    if ((((a1[0] - 0x1010101) & ~a1[0]) & 0x80808080) != 0)
                        return 0;
                    ++a1;
                    ++a2;
                }

                /// A difference was detected in last few bytes of s1, so search bytewise
                string1 = (sbyte*)a1;
                string2 = (sbyte*)a2;
            }

            while (string1[0] != '\0' && string1[0] == string2[0])
            {
                ++string1;
                ++string2;
            }
            return string1[0] - string2[0];
        }

        #endregion strcmp

        #region strcoll

        /// <summary>Compare strings using the current locale or a specified LC_CTYPE conversion state category.</summary>
        /// <param name="string1">Null-terminated strings to compare.</param>
        /// <param name="string2">Null-terminated strings to compare.</param>
        /// <returns>
        /// Each of these functions returns a value indicating the relationship of string1 to string2, as follows.
        /// < 0 string1 less than string2 
        /// 0 string1 identical to string2
        /// > 0 string1 greater than string2
        /// </returns>
        unsafe public static int strcoll(sbyte* string1, sbyte* string2)
        {
            return strcmp(string1, string2);
        }

        #endregion strcoll

        #region strcpy
        /// <summary>Copy a string.</summary>
        /// <param name="strDestination">Destination string.</param>
        /// <param name="strSource">Null-terminated source string.</param>
        /// <returns>Each of these functions returns the destination string. No return value is reserved to indicate an error.</returns>
        /// <remarks>
        /// The strcpy function copies strSource, including the terminating null character, to the location specified by strDestination.
        /// No overflow checking is performed when strings are copied or appended.
        /// The behavior of strcpy is undefined if the source and destination strings overlap.
        /// </remarks>
        /// <Security>
        /// The first argument, strDestination, must be large enough to hold strSource and the closing '\0'; otherwise, a buffer overrun can occur.
        /// This can lead to a denial of service attack against the application if an access violation occurs,
        /// or in the worst case, allow an attacker to inject executable code into your process.
        /// This is especially true if strDestination is a stack-based buffer.
        /// Consider using strncpy or wcsncpy, or consider using an appropriate strsafe function.
        /// </Security>
        unsafe public static sbyte* strcpy(sbyte* strDestination, sbyte* strSource)
        {
            sbyte* ret = strDestination;
            if ((((uint)strDestination | (uint)strSource) & (LITTLE_BLOCK_SIZE - 1)) == 0)
            {
                uint v = ((uint*)strSource)[0];
                if ((((v - 0x1010101) & ~v) & 0x80808080) == 0)
                {
                    do
                    {
                        ((uint*)strDestination)[0] = v;
                        strDestination = (sbyte*)((int)strDestination + 4);
                        strSource = (sbyte*)((int)strSource + 4);
                        v = ((uint*)strSource)[0];
                    }
                    while ((((v - 0x1010101) & ~v) & 0x80808080) == 0);
                }
            }

            sbyte srcb = strSource[0];
            if (srcb != 0)
            {
                do
                {
                    strDestination[0] = srcb;
                    ++strDestination;
                    ++strSource;
                    srcb = strSource[0];
                } while (srcb != 0);
            }

            return ret;
        }

        #endregion strcpy

        #region strcspn

        /// <summary>Find a substring in a string.</summary>
        /// <param name="str">Null-terminated searched string.</param>
        /// <param name="strCharSet">Null-terminated character set.</param>
        /// <returns>
        /// Each of these functions returns an integer value specifying the length of the initial segment of string
        /// that consists entirely of characters not in strCharSet.
        /// If string begins with a character that is in strCharSet, the function returns 0.
        /// No return value is reserved to indicate an error.
        /// </returns>
        /// <remarks>
        /// The strcspn function returns the index of the first occurrence of a character in string that belongs
        /// to the set of characters in strCharSet.
        /// Terminating null characters are included in the search.
        /// </remarks>
        /// <LWQ>从第一个字符串中寻找第二个字符串的各个字符，找到就停止</LWQ>
        unsafe public static uint strcspn(sbyte* str, sbyte* strCharSet)
        {
            sbyte* s = str;
            sbyte* c;
            while (str[0] != 0)
            {
                for (c = strCharSet; c[0] != 0; c++)
                {
                    if (str[0] == c[0])
                        break;
                }
                if (c[0] != 0)
                    break;
                str++;
            }
            return (uint)str - (uint)s;
        }

        #endregion strcspn

        #region strlen

        /// <summary>Get the length of a string.</summary>
        /// <param name="String">Null-terminated string. </param>
        /// <returns>
        /// Each of these functions returns the number of characters in string, excluding the terminal NULL.
        /// No return value is reserved to indicate an error.
        /// </returns>
        /// <remarks>
        /// Each of these functions returns the number of characters in string, not including the terminating null character
        /// </remarks>
        /// <Security>
        /// The argument, string, must not be larger than the maximum number of bytes allowed in the buffer;
        /// otherwise, a buffer overrun can occur. 
        /// This can lead to a denial of service attack against the application if an access violation occurs, 
        /// or in the worst case, allow an attacker to inject executable code into your process.
        /// Consider using an appropriate strsafe function.
        /// </Security>
        unsafe public static uint strlen(sbyte* String)
        {
            sbyte* start = String;
            if (((uint)start & (LITTLE_BLOCK_SIZE - 1)) == 0)
            {
                while ((((((uint*)String)[0] - 0x1010101) & ~((uint*)String)[0]) & 0x80808080) == 0)
                {
                    String = (sbyte*)((int)String + 4);
                }
            }
            sbyte b = String[0];
            if (b != 0)
            {
                do
                {
                    ++String;
                    b = String[0];
                }
                while (b != 0);
            }
            return (uint)String - (uint)start;
        }

        #endregion strlen


        ///////////////////////////////////////
        //以下函数没进行过仔细优化,只进行过初步数值测试

        #region strncat

        /// <summary>Append characters of a string.</summary>
        /// <param name="strDest">Null-terminated destination string.</param>
        /// <param name="strSource">Null-terminated source string.</param>
        /// <param name="count">Number of characters to append.</param>
        /// <returns>Each of these functions returns a pointer to the destination string. No return value is reserved to indicate an error.</returns>
        /// <remarks>
        /// The strncat function appends, at most, the first count characters of strSource to strDest.
        /// The initial character of strSource overwrites the terminating null character of strDest. 
        /// If a null character appears in strSource before count characters are appended, strncat appends all characters from strSource,
        /// up to the null character. 
        /// If count is greater than the length of strSource, the length of strSource is used in place of count.
        /// The resulting string is terminated with a null character. 
        /// If copying takes place between strings that overlap, the behavior is undefined.
        /// </remarks>
        /// <Security>
        /// The first argument, strDest, must be large enough to hold the current strDest and strSource combined and a closing NULL ('\0');
        /// otherwise, a buffer overrun can occur. 
        /// This can lead to a denial of service attack against the application if an access violation occurs, or in the worst case,
        /// allow an attacker to inject executable code into your process. This is especially true if strDest is a stack-based buffer.
        /// The last argument, count, is the number of bytes to copy into strDest, not the size of strDest. 
        /// </Security>
        unsafe public static sbyte* strncat(sbyte* strDest, sbyte* strSource, uint count)
        {
            sbyte* s = strDest;
            /// Skip over the data in s1 as quickly as possible.
            if (((uint)strDest & (LITTLE_BLOCK_SIZE - 1)) == 0)
            {
                uint* aligned_s1 = (uint*)strDest;
                while ((((aligned_s1[0] - 0x1010101) & ~aligned_s1[0]) & 0x80808080) == 0)
                    aligned_s1++;
                strDest = (sbyte*)aligned_s1;
            }
            while (strDest[0] != 0)
                strDest++;
            /// s1 now points to the its trailing null character, now copy up to N bytes from S2 into S1 stopping if a NULL is encountered in S2.
            /// It is not safe to use strncpy here since it copies EXACTLY N characters, NULL padding if necessary.
            while (count-- != 0 && (*strDest++ = *strSource++) != 0)
            {
                if (count == 0)
                    strDest[0] = (sbyte)'\0';
            }
            return s;
        }

        #endregion strncat

        #region strncmp

        /// <summary>Compare characters of two strings.</summary>
        /// <param name="string1">Strings to compare.</param>
        /// <param name="string2">Strings to compare.</param>
        /// <param name="count">Number of characters to compare.</param>
        /// <returns>
        /// The return value indicates the relation of the substrings of string1 and string2 as follows.
        /// < 0 string1 substring less than string2 substring 
        /// 0 string1 substring identical to string2 substring 
        /// > 0 string1 substring greater than string2 substring 
        /// </returns>
        /// <remarks>
        /// The strncmp function lexicographically compares, at most, 
        /// the first count characters in string1 and string2 and returns a value indicating the relationship between the substrings.
        /// strncmp is case-sensitive. Unlike strcoll, strncmp is not affected by locale.
        /// </remarks>
        unsafe public static int strncmp(sbyte* string1, sbyte* string2, uint count)
        {
            uint* a1;
            uint* a2;
            if (count == 0) return 0;

            /// If s1 or s2 are unaligned, then compare bytes.
            if ((((uint)string1 & (LITTLE_BLOCK_SIZE - 1)) | ((uint)string2 & (LITTLE_BLOCK_SIZE - 1))) == 0)
            {
                /// If s1 and s2 are word-aligned, compare them a word at a time.
                a1 = (uint*)string1;
                a2 = (uint*)string2;
                while (count >= CStdlib.INT_SIZE && a1[0] == a2[0])
                {
                    count -= CStdlib.INT_SIZE;
                    /// If we've run out of bytes or hit a null, return zero since we already know *a1 == *a2.
                    if (count == 0 || (((a1[0]) - 0x01010101) & ~(a1[0]) & 0x80808080) != 0)
                        return 0;
                    a1++;
                    a2++;
                }
                /// A difference was detected in last few bytes of s1, so search bytewise
                string1 = (sbyte*)a1;
                string2 = (sbyte*)a2;
            }

            while (count-- > 0 && string1[0] == string2[0])
            {
                /// If we've run out of bytes or hit a null, return zero since we already know *s1 == *s2.
                if (count == 0 || *string1 == '\0')
                    return 0;
                string1++;
                string2++;
            }
            return (*(byte*)string1) - (*(byte*)string2);
        }

        #endregion strncmp

        #region strncpy

        /// <summary>Copy characters of one string to another.</summary>
        /// <param name="strDest">Destination string.</param>
        /// <param name="strSource">Source string.</param>
        /// <param name="count">Number of characters to be copied.</param>
        /// <returns>Each of these functions returns strDest. No return value is reserved to indicate an error.</returns>
        /// <remarks>
        /// The strncpy function copies the initial count characters of strSource to strDest and returns strDest.
        /// If count is less than or equal to the length of strSource, a null character is not appended automatically to the copied string.
        /// If count is greater than the length of strSource, the destination string is padded with null characters up to length count.
        /// The behavior of strncpy is undefined if the source and destination strings overlap.
        /// </remarks>
        /// <Security>
        /// The first argument, strDest, must be large enough to hold strSource and the closing '\0'; otherwise, a buffer overrun can occur.
        /// This can lead to a denial of service attack against the application if an access violation occurs,
        /// or in the worst case, allow an attacker to inject executable code into your process.
        /// This is especially true if strDest is a stack-based buffer. Consider using an appropriate strsafe function.
        /// </Security>
        unsafe public static sbyte* strncpy(sbyte* strDest, sbyte* strSource, uint count)
        {
            sbyte* dst = strDest;
            sbyte* src = strSource;
            int* aligned_dst;
            int* aligned_src;

            /// If SRC and DEST is aligned and count large enough, then copy words.  */
            if (
                ((((uint)src & (CStdlib.INT_SIZE - 1)) | ((uint)dst & (CStdlib.INT_SIZE - 1))) == 0) &&
                count > CStdlib.INT_SIZE
                )
            {
                aligned_dst = (int*)dst;
                aligned_src = (int*)src;

                /// SRC and DEST are both "int" aligned, try to do "int" sized copies.
                while (
                    count >= CStdlib.INT_SIZE &&
                    (((aligned_src[0]) - 0x01010101) & ~(aligned_src[0]) & 0x80808080) == 0
                    )
                {
                    count -= CStdlib.INT_SIZE;
                    *aligned_dst++ = *aligned_src++;
                }

                dst = (sbyte*)aligned_dst;
                src = (sbyte*)aligned_src;
            }

            while (count > 0)
            {
                --count;
                if ((*dst++ = *src++) == '\0')
                    break;
            }

            while (count-- > 0)
                *dst++ = (sbyte)'\0';
            return strDest;
        }

        #endregion strncpy

        #region strrchr

        /// <summary>Scan a string for the last occurrence of a character.</summary>
        /// <param name="str">Null-terminated string to search.</param>
        /// <param name="c">Character to be located.</param>
        /// <returns>Each of these functions returns a pointer to the last occurrence of c in string, or NULL if c is not found.</returns>
        /// <remarks>The strrchr function finds the last occurrence of c (converted to char) in string. The search includes the terminating null character.</remarks>
        unsafe public static sbyte* strrchr(sbyte* str, int c)
        {
            sbyte* last = null;
            if (c != 0)
            {
                while ((str = strchr(str, c)) != null)
                {
                    last = str;
                    str++;
                }
            }
            else
            {
                last = strchr(str, c);
            }
            return last;
        }

        #endregion strrchr

        #region strspn

        /// <summary>Find the first substring.</summary>
        /// <param name="str">Null-terminated string to search.</param>
        /// <param name="strCharSet">Null-terminated character set.</param>
        /// <returns>
        /// strspn and wcsspn return an integer value specifying the length of the substring in string that consists entirely of characters in strCharSet.
        /// If string begins with a character not in strCharSet, the function returns 0. No return value is reserved to indicate an error.
        /// </returns>
        /// <remarks>
        /// The strspn function returns the index of the first character in string that does not belong to the set of characters in strCharSet. 
        /// The search does not include terminating null characters.
        /// </remarks>
        unsafe public static uint strspn(sbyte* str, sbyte* strCharSet)
        {
            sbyte* s = str;
            sbyte* c;
            while (str[0] != 0)
            {
                for (c = strCharSet; c[0] != 0; c++)
                {
                    if (str[0] == c[0])
                        break;
                }
                if (c[0] == '\0')
                    break;
                str++;
            }
            return (uint)str - (uint)s;
        }

        #endregion strspn

        #region strstr

        /// <summary>Find a substring.</summary>
        /// <param name="str">Null-terminated string to search.</param>
        /// <param name="strCharSet">Null-terminated string to search for.</param>
        /// <returns>
        /// Each of these functions returns a pointer to the first occurrence of strCharSet in string, 
        /// or NULL if strCharSet does not appear in string.
        /// If strCharSet points to a string of zero length, the function returns string.
        /// </returns>
        /// <remarks>
        /// The strstr function returns a pointer to the first occurrence of strCharSet in string.
        /// The search does not include terminating null characters.
        /// </remarks>
        unsafe public static sbyte* strstr(sbyte* str, sbyte* strCharSet)
        {
            if (str[0] == 0)
            {
                if (strCharSet[0] != 0)
                    return null;
                return str;
            }
            while (str[0] != 0)
            {
                uint i = 0;
                while (true)
                {
                    if (strCharSet[i] == 0)
                        return str;
                    if (strCharSet[i] != str[i])
                        break;
                    i++;
                }
                str++;
            }
            return null;
        }

        #endregion strstr

        #region strtok 未实现

        unsafe private static sbyte* strtok_r(sbyte* s, sbyte* delim, sbyte** lasts)
        {
            sbyte* spanp;
            int c, sc;
            sbyte* tok;

            if (s == null && (s = *lasts) == null)
                return (null);

            /// Skip (span) leading delimiters (s += strspn(s, delim), sort of).
        cont:
            c = *s++;
            for (spanp = (sbyte*)delim; (sc = *spanp++) != 0; )
            {
                if (c == sc)
                    goto cont;
            }

            if (c == 0)
            {
                /// no non-delimiter sbyte acters
                *lasts = null;
                return (null);
            }
            tok = s - 1;

            /// Scan token (scan for delimiters: s += strcspn(s, delim), sort of).
            /// Note that delim must have one NUL; we stop if we see that, too.
            for (; ; )
            {
                c = *s++;
                spanp = (sbyte*)delim;
                do
                {
                    if ((sc = *spanp++) == c)
                    {
                        if (c == 0)
                            s = null;
                        else
                            s[-1] = 0;
                        *lasts = s;
                        return (tok);
                    }
                } while (sc != 0);
            }
            /// NOTREACHED
        }

        #endregion strtok 未实现

        #region strxfrm

        /// <summary>Transform a string based on locale-specific information.</summary>
        /// <param name="strDest">Destination string.</param>
        /// <param name="strSource">Source string.</param>
        /// <param name="count">Maximum number of characters to place in strDest.</param>
        /// <returns>
        /// Returns the length of the transformed string, not counting the terminating null character.
        /// If the return value is greater than or equal to count, the content of strDest is unpredictable.
        /// On an error, each function sets errno and returns INT_MAX.
        /// For an invalid character, errno is set to EILSEQ.
        /// </returns>
        /// <remarks>
        /// The strxfrm function transforms the string pointed to by strSource into a new collated form that is stored in strDest.
        /// No more than count characters, including the null character, are transformed and placed into the resulting string.
        /// The transformation is made using the locale's LC_COLLATE category setting. For more information on LC_COLLATE, see setlocale.
        /// strxfrm uses the current locale for its locale-dependent behavior;
        /// _strxfrm_l is identical except that it uses the locale passed in instead of the current locale.
        /// After the transformation, a call to strcmp with the two transformed strings yields results
        /// identical to those of a call to strcoll applied to the original two strings.
        /// As with strcoll and stricoll, strxfrm automatically handles multibyte-character strings as appropriate.
        /// </remarks>
        unsafe public static uint strxfrm(sbyte* strDest, sbyte* strSource, uint count)
        {
            uint res;
            res = 0;
            while (count-- > 0)
            {
                if ((*strDest++ = *strSource++) != '\0')
                    ++res;
                else
                    return res;
            }
            while (strSource[0] != 0)
            {
                ++strSource;
                ++res;
            }
            return res;
        }

        #endregion strxfrm
    }
    #endregion //CString

    #region CType
    public static class CType
    {
        const int _U = 001;
        const int _L = 002;
        const int _N = 004;
        const int _S = 008;
        const int _P = 016;
        const int _C = 032;
        const int _X = 064;
        const int _B = 128;
        const int _Z = 0;

        static readonly byte[] _CTYPE_DATA = new byte[256] 
        {
            _C, _C, _C, _C, _C, _C, _C, _C, 
            _C, _C | _S, _C | _S, _C | _S, _C | _S, _C | _S, _C, _C,
            _C, _C, _C, _C, _C, _C, _C, _C,
            _C, _C, _C, _C, _C, _C, _C, _C,
            _S | _B, _P, _P, _P, _P, _P, _P, _P,
            _P, _P, _P, _P, _P, _P, _P, _P,
            _N, _N, _N, _N, _N, _N, _N, _N,
            _N, _N, _P, _P, _P, _P, _P, _P,
            _P, _U | _X, _U | _X, _U | _X, _U | _X, _U | _X, _U | _X, _U,
            _U, _U, _U, _U, _U, _U, _U, _U,
            _U, _U, _U, _U, _U, _U, _U, _U,
            _U, _U, _U, _P, _P, _P, _P, _P,
            _P, _L | _X, _L | _X, _L | _X, _L | _X, _L | _X, _L | _X, _L,
            _L, _L, _L, _L, _L, _L, _L, _L,
            _L, _L, _L, _L, _L, _L, _L, _L,
            _L, _L, _L, _P, _P, _P, _P, _C,
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z,
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z, 
            _Z, _Z, _Z, _Z, _Z, _Z, _Z, _Z
        };

        public static int isalnum(int c) { return (_CTYPE_DATA[c] & (_U | _L | _N)); }
        public static int isalpha(int c) { return (_CTYPE_DATA[c] & (_U | _L)); }
        public static int isascii(int c) { return (_CTYPE_DATA[c]); }
        public static int iscntrl(int c) { return (_CTYPE_DATA[c] & _C); }
        public static int isdigit(int c) { return (_CTYPE_DATA[c] & _N); }
        public static int islower(int c) { return (_CTYPE_DATA[c] & _L); }
        public static int isgraph(int c) { return (_CTYPE_DATA[c] & (_P | _U | _L | _N)); }
        public static int isprint(int c) { return (_CTYPE_DATA[c] & (_P | _U | _L | _N | _B)); }
        public static int ispunct(int c) { return (_CTYPE_DATA[c] & _P); }
        public static int isspace(int c) { return (_CTYPE_DATA[c] & _S); }
        public static int isupper(int c) { return (_CTYPE_DATA[c] & _U); }
        public static int isxdigit(int c) { return (_CTYPE_DATA[c] & ((_X) | (_N))); }
        public static int toascii(int c) { return (c) & 0xFF; }
        public static int tolower(int c) { return isupper(c) != 0 ? (c) - 'A' + 'a' : c; }
        public static int toupper(int c) { return islower(c) != 0 ? c - 'a' + 'A' : c; }
    }
    #endregion //CType

    #region CUtil
    public static class CUtil
    {
        public static int DIV(int X, int Y)
        {
            int Z = 0;
            while (X >= Y)
            {
                X -= Y;
                ++Z;
            }
            return Z;
        }
        public static uint DIV(uint X, uint Y)
        {
            uint Z = 0;
            while (X >= Y)
            {
                X -= Y;
                ++Z;
            }
            return Z;
        }
        public static uint DIV(ulong S, uint Z)
        {
            uint X = (uint)(S >> 32);
            uint Y = (uint)S;
            //y保存商 x保存余数 
            for (int I = 0; I < 32; ++I)
            {
                uint T = (uint)((int)X >> 31);
                X = (X << 1) | (Y >> 31);
                Y = Y << 1;
                if ((X | T) >= Z)
                {
                    X -= Z;
                    ++Y;
                }
            }
            return Y;
        }
        public static int MOD(int X, int Y)
        {
            while (X >= Y)
                X -= Y;
            return X;
        }
        public static uint MOD(uint X, uint Y)
        {
            while (X >= Y)
                X -= Y;
            return X;
        }
    }
    #endregion //CUtil

    #region AnsiString
    public class AnsiString
    {
        ///////////////////////////////////////

        /// <summary>空字符实例</summary>
        public static readonly AnsiString Empty = new AnsiString(1);

        /// <summary>按内容比较两个字符串是否相同</summary>
        public static int Compare(AnsiString strA, AnsiString strB)
        {
            if (strA.Length != strB.Length) return strA.Length - strB.Length;
            unsafe
            {
                fixed (byte* pA = &strA._array[0])
                fixed (byte* pB = &strB._array[0])
                    return CString.memcmp(pA, pB, (uint)strA.Length);
            }
        }

        /// <summary>从新克隆一份对象(不是复制一份引用)</summary>
        public static AnsiString Copy(AnsiString str)
        {
            byte[] buffer = new byte[str.Length];
            Array.Copy(str._array, buffer, str.Length);
            return new AnsiString(buffer);
        }

        /// <summary>首先按引用比较,然后按内容比较</summary>
        public static bool Equals(string a, string b)
        {
            if (a == b) return true;
            if ((a != null) && (b != null)) return a.Equals(b);
            return false;
        }

        /// <summary>首先按引用比较,然后按内容比较</summary>
        public static bool operator ==(AnsiString a, AnsiString b)
        {
            return AnsiString.Equals(a, b);
        }

        /// <summary>首先按引用比较,然后按内容比较</summary>
        public static bool operator !=(AnsiString a, AnsiString b)
        {
            return !AnsiString.Equals(a, b);
        }

        ///////////////////////////////////////

        private byte[] _array;  //内部缓冲

        /// <summary>取得每个字符</summary>
        public byte this[int Index] { get { return this._array[Index]; } }

        /// <summary>字符串长度</summary>
        public int Length { get { return this._array.Length; } }

        /// <summary>建立缓冲,但字符内容为空,缓冲都是0</summary>
        public AnsiString(int Capacity) { this._array = new byte[Capacity]; }

        /// <summary>用外部缓冲建立字符串,相同引用</summary>
        unsafe public AnsiString(byte[] Value) { this._array = Value; }

        /// <summary>根据外部缓冲建立字符串,复制其内容</summary>
        unsafe public AnsiString(byte* pValue, int Length)
        {
            this._array = new byte[Length];
            Marshal.Copy((IntPtr)pValue, this._array, 0, Length);
        }

        /// <summary>用外部字符串的内容建立一个字符串,内容完整</summary>
        unsafe public AnsiString(string s)
        {
            int count = s.Length;
            byte[] buffer = new byte[count];
            fixed (byte* dst0 = &buffer[0]) fixed (char* src0 = s)
            {
                ushort* src = (ushort*)src0;
                byte* dst = (byte*)dst0;
                while (count >= 4)
                {
                    ((byte*)((int)dst + 0))[0] = (byte)(((short*)((int)src + 0))[0]);
                    ((byte*)((int)dst + 1))[0] = (byte)(((short*)((int)src + 2))[0]);
                    ((byte*)((int)dst + 2))[0] = (byte)(((short*)((int)src + 4))[0]);
                    ((byte*)((int)dst + 3))[0] = (byte)(((short*)((int)src + 6))[0]);
                    src = (ushort*)((int)src + 8);
                    dst = (byte*)((int)dst + 4);
                    count -= 4;
                }
                while (count-- != 0)
                {
                    ((byte*)dst)[0] = (byte)((ushort*)src)[0];
                    src = (ushort*)((int)src + 2);
                    dst = (byte*)((int)dst + 1);
                }
            }
            this._array = buffer;
        }

        /// <summary>把内部缓冲复制外部</summary>
        public void CopyTo(int sourceIndex, byte[] destination, int destinationIndex, int count)
        {
            if (destination == null) throw new ArgumentNullException();
            if (count < 0) throw new ArgumentOutOfRangeException();
            if (sourceIndex < 0) throw new ArgumentOutOfRangeException();
            if (count > (this.Length - sourceIndex)) throw new ArgumentOutOfRangeException();
            if ((destinationIndex > (destination.Length - count)) || (destinationIndex < 0)) throw new ArgumentOutOfRangeException();
            Array.Copy(this._array, destination, count);
        }

        private bool EndsWith(byte value)
        {
            if ((this.Length != 0) && (this[this.Length - 1] == value))
                return true;
            return false;
        }
        /// <summary>strcmp优化</summary>
        public bool EndsWith(AnsiString value)
        {
            if (value.Length > this.Length) return false;
            unsafe
            {
                fixed (byte* pA = &this._array[this.Length - value.Length])
                fixed (byte* pB = &value._array[0])
                    if (CString.strncmp((sbyte*)pA, (sbyte*)pB, (uint)value.Length) == 0)
                        return true;
            }
            return false;
        }

        /// <summary>首先转换成AnsiString,然后按引用比较,然后按内容比较</summary>
        public override bool Equals(object obj)
        {
            if (obj is AnsiString) return AnsiString.Equals(this, (AnsiString)obj);
            return false;
        }

        /// <summary>按内容比较</summary>
        public bool Equals(AnsiString value)
        {
            if (value == null) return false;
            if (this.Length != value.Length) return false;
            return (AnsiString.Compare(this, value) == 0);
        }

        public int IndexOfAny(byte[] anyOf, int startIndex, int count)
        {
            if ((startIndex < 0) || (startIndex > this.Length)) throw new ArgumentOutOfRangeException();
            if ((count < 0) || (count > (this.Length - startIndex))) throw new ArgumentOutOfRangeException();
            if (anyOf == null) throw new ArgumentNullException();

            int len = startIndex + count;
            for (int i = startIndex; i < len; i++)
                for (int j = 0; j < anyOf.Length; j++)
                    if (this._array[i] == anyOf[j])
                        return i;
            return -1;
        }

        public AnsiString Insert(int startIndex, AnsiString value)
        {
            if (value == null) throw new ArgumentNullException();
            if ((startIndex < 0) || (startIndex > this.Length)) throw new ArgumentOutOfRangeException();

            int len = value.Length + this.Length;
            byte[] buffer = new byte[len];
            Array.Copy(value._array, 0, buffer, startIndex, value.Length);
            Array.Copy(this._array, 0, buffer, 0, startIndex);
            Array.Copy(this._array, this._array.Length - startIndex, buffer, startIndex + value.Length, this._array.Length - startIndex);
            return new AnsiString(buffer);
        }

        public AnsiString Remove(int startIndex, int count)
        {
            if (((startIndex < 0) || (count < 0)) || ((startIndex > this.Length) || ((startIndex + count) > this.Length)))
                throw new ArgumentException();
            byte[] buffer = new byte[this.Length - count];
            Array.Copy(this._array, 0, buffer, 0, startIndex);
            Array.Copy(this._array, startIndex + count, buffer, startIndex, this.Length - startIndex - count);
            return new AnsiString(buffer);
        }

        public AnsiString Replace(byte oldChar, byte newChar)
        {
            byte[] buffer = new byte[this.Length];
            for (int i = 0; i < this.Length; i++)
                buffer[i] = (this._array[i] == oldChar) ? newChar : this._array[i];
            return new AnsiString(buffer);
        }

        /// <summary>克隆多一份内部数据</summary>
        public byte[] ToByteArray()
        {
            byte[] array = new byte[this._array.Length];
            Array.Copy(this._array, array, this._array.Length);
            return array;
        }

        unsafe public override string ToString()
        {
            //return System.Text.Encoding.ASCII.GetString(this._Array);
            int count = this._array.Length;
            string ret = new string((char)0, count);
            fixed (char* dest0 = ret) fixed (byte* src0 = &this._array[0])
            {
                ushort* dest = (ushort*)dest0;
                byte* src = (byte*)src0;
                while (count >= 4)
                {
                    ((short*)((int)dest + 0))[0] = ((byte*)((int)src + 0))[0];
                    ((short*)((int)dest + 2))[0] = ((byte*)((int)src + 1))[0];
                    ((short*)((int)dest + 4))[0] = ((byte*)((int)src + 2))[0];
                    ((short*)((int)dest + 6))[0] = ((byte*)((int)src + 3))[0];
                    dest = (ushort*)((int)dest + 8);
                    src = (byte*)((int)src + 4);
                    count -= 4;
                }
                while (count-- != 0)
                {
                    ((ushort*)dest)[0] = ((byte*)src)[0];
                    dest = (ushort*)((int)dest + 2);
                    src = (byte*)((int)src + 1);
                }
            }
            return ret;
        }

        public override int GetHashCode()
        {
            return this._array.GetHashCode();
        }

        ///////////////////////////////////////

    }
    #endregion //AnsiString
}
