using ProtoBuf;
using PureFood.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.Models.Shared
{
    [ProtoContract]
    public class KeyValueTypeIntModel
    {
        [ProtoMember(1)] public int Value { get; set; }
        public int Id => Value;

        [ProtoMember(2)] public string Text { get; set; }

        [ProtoMember(3)] public bool Checked { get; set; }
        [ProtoMember(4)] public int Level { get; set; }
        public string Label => Text;
        public bool Selected => Checked;
        public object? Ext { get; set; }
        public object? Ext2 { get; set; }
        public object? Ext3 { get; set; }

        public static KeyValueTypeIntModel[] FromEnum(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;
            KeyValueTypeIntModel[] keyValueModels = new KeyValueTypeIntModel[length];
            int i = 0;
            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value),
                };
                i++;
            }

            return keyValueModels;
        }

        public static KeyValueTypeIntModel[] FromEnum(Type enumType, int[] exclusValues)
        {
            var values = Enum.GetValues(enumType);
            List<KeyValueTypeIntModel> keyValueModels = new List<KeyValueTypeIntModel>();
            foreach (var value in values)
            {
                if (exclusValues.Contains((int)value))
                {
                    continue;
                }

                keyValueModels.Add(new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value),
                });
            }

            return keyValueModels.ToArray();
        }

        public static KeyValueTypeIntModel[] FromEnumSelectMultiple(Type enumType, int allStatus)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;


            KeyValueTypeIntModel[] keyValueModels = new KeyValueTypeIntModel[length];
            int i = 0;


            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value),
                    Checked = (allStatus & (int)value) == (int)value
                };
                i++;
            }

            return keyValueModels;
        }

        public static KeyValueTypeIntModel[] FromEnumSelectMultiple(Type enumType, int[]? allStatus)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;
            KeyValueTypeIntModel[] keyValueModels = new KeyValueTypeIntModel[length];
            int i = 0;
            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value),
                    Checked = allStatus?.Contains((int)value) == true
                };
                i++;
            }

            return keyValueModels;
        }

        public static KeyValueTypeIntModel[] FromEnum(Type enumType, int selectedValue)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;

            KeyValueTypeIntModel[] keyValueModels = new KeyValueTypeIntModel[length];
            int i = 0;

            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value),
                    Checked = selectedValue == ((int)value)
                };
                i++;
            }

            return keyValueModels;
        }

        public static KeyValueTypeIntModel[] FromEnumSelectMultiple(Type enumType, Enum? allStatus)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;
            KeyValueTypeIntModel[] keyValueModels = new KeyValueTypeIntModel[length];
            int i = 0;
            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value),
                    Checked = allStatus?.HasFlag((Enum)value) == true
                };
                i++;
            }

            return keyValueModels;
        }

        public static KeyValueTypeIntModel[] FromEnumSelectMultiple(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            int length = values.Length;
            KeyValueTypeIntModel[] keyValueModels = new KeyValueTypeIntModel[length];
            int i = 0;
            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value)
                };
                i++;
            }

            return keyValueModels;
        }

        public static KeyValueTypeIntModel[] FromEnumSelectMultiple(Array values, Enum? currentStatus)
        {
            int length = values.Length;
            KeyValueTypeIntModel[] keyValueModels = new KeyValueTypeIntModel[length];
            int i = 0;
            foreach (var value in values)
            {
                keyValueModels[i] = new KeyValueTypeIntModel()
                {
                    Text = ((Enum)value).GetDisplayName(),
                    Value = ((int)value),
                    Checked = (Equals(currentStatus, (Enum)value))
                };
                i++;
            }

            return keyValueModels;
        }
    }
}
