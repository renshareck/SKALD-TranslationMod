using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
[Serializable]
public static class Calendar
{
	// Token: 0x06000B41 RID: 2881 RVA: 0x00035CDA File Offset: 0x00033EDA
	public static void setCalendar()
	{
		MainControl.log("Setting Calendar!");
		Calendar.addYears(2997);
		Calendar.addDays(150);
		Calendar.addHours(12);
		Calendar.startDay = 150UL;
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x00035D0F File Offset: 0x00033F0F
	public static void setCalendar(int _years, ulong _seconds)
	{
		Calendar.seconds = _seconds;
		Calendar.year = _years;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x00035D1D File Offset: 0x00033F1D
	public static string addSec(int sec)
	{
		Calendar.seconds += (ulong)((long)sec);
		while (Calendar.seconds >= 33696000UL)
		{
			Calendar.seconds -= 33696000UL;
			Calendar.addYears(1);
		}
		return sec.ToString();
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x00035D5A File Offset: 0x00033F5A
	public static string addMin(string min)
	{
		return Calendar.addMin(Calendar.parse(min));
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x00035D67 File Offset: 0x00033F67
	public static string addMin(int min)
	{
		Calendar.addSec(min * 60);
		return min.ToString();
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00035D7A File Offset: 0x00033F7A
	public static string addHours(string hours)
	{
		return Calendar.addHours(Calendar.parse(hours));
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x00035D87 File Offset: 0x00033F87
	public static string addHours(int hours)
	{
		Calendar.addMin(hours * 60);
		return hours.ToString();
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x00035D9C File Offset: 0x00033F9C
	public static void setHour(string target)
	{
		int num = Calendar.parse(target);
		if (num < 0 || num > 24)
		{
			return;
		}
		while (Calendar.getHourInt() != num)
		{
			Calendar.addHours(1);
		}
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00035DC9 File Offset: 0x00033FC9
	public static string addDays(string days)
	{
		return Calendar.addDays(Calendar.parse(days));
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x00035DD6 File Offset: 0x00033FD6
	public static string addDays(int days)
	{
		Calendar.addHours(days * 24);
		return days.ToString();
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x00035DE9 File Offset: 0x00033FE9
	public static string addYears(string years)
	{
		return Calendar.addYears(Calendar.parse(years));
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00035DF6 File Offset: 0x00033FF6
	public static string addYears(int years)
	{
		Calendar.year += years;
		return years.ToString();
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00035E0B File Offset: 0x0003400B
	public static ulong getMinutesInt()
	{
		return Calendar.seconds / 60UL % 60UL;
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00035E1A File Offset: 0x0003401A
	public static int getYear()
	{
		return Calendar.year;
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x00035E24 File Offset: 0x00034024
	public static string getMinutes()
	{
		return Calendar.zeroFormatString(Calendar.getMinutesInt().ToString());
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x00035E43 File Offset: 0x00034043
	public static string zeroFormatString(string s)
	{
		if (s.Length == 1)
		{
			s = "0" + s;
		}
		return s;
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x00035E5C File Offset: 0x0003405C
	public static int getHourInt()
	{
		return (int)(Calendar.seconds / 3600UL) % 24;
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x00035E70 File Offset: 0x00034070
	public static string getHour()
	{
		return Calendar.getHourInt().ToString();
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00035E8A File Offset: 0x0003408A
	private static ulong getDayInt()
	{
		return Calendar.seconds / 86400UL;
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00035E98 File Offset: 0x00034098
	public static ulong getDayTotal()
	{
		return (ulong)((long)Calendar.getYear() * 390L + (long)Calendar.getDayInt());
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x00035EAD File Offset: 0x000340AD
	public static int getHoursPerDay()
	{
		return 24;
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x00035EB4 File Offset: 0x000340B4
	public static string getDay()
	{
		return Calendar.getDayInt().ToString();
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00035ED0 File Offset: 0x000340D0
	public static string getTime()
	{
		string hour = Calendar.getHour();
		string minutes = Calendar.getMinutes();
		return Calendar.zeroFormatString(hour) + ":" + minutes;
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00035EF8 File Offset: 0x000340F8
	public static string printDayTime()
	{
		return "" + "Day " + Calendar.getDay() + " (" + Calendar.getTime() + "). ";
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00035F22 File Offset: 0x00034122
	public static string printDayTimeFormatted()
	{
		return "" + TextTools.formateNameValuePairYellow("Time:", Calendar.getTime() + "\n") + TextTools.formateNameValuePairYellow("Day:", Calendar.getDay());
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00035F5B File Offset: 0x0003415B
	public static string getSaveDateStamp()
	{
		return string.Concat(new string[]
		{
			Calendar.getDay(),
			"_",
			Calendar.getHour(),
			"_",
			Calendar.getMinutes()
		});
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x00035F90 File Offset: 0x00034190
	public static string getDayTimeString()
	{
		if (Calendar.getTimeOfDay() == Calendar.TimeOfDay.NIGHT)
		{
			return "Night";
		}
		if (Calendar.getTimeOfDay() == Calendar.TimeOfDay.DUSK)
		{
			return "Dusk";
		}
		if (Calendar.getTimeOfDay() == Calendar.TimeOfDay.DAWN)
		{
			return "Dawn";
		}
		return "Day";
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x00035FC0 File Offset: 0x000341C0
	public static string getDaysSinceStart()
	{
		return (Calendar.getDayInt() - Calendar.startDay).ToString();
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x00035FE0 File Offset: 0x000341E0
	public static bool isItNight()
	{
		return Calendar.getTimeOfDay() == Calendar.TimeOfDay.NIGHT;
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00035FEC File Offset: 0x000341EC
	private static Calendar.TimeOfDay getTimeOfDay()
	{
		int hourInt = Calendar.getHourInt();
		if (hourInt < 19 && hourInt >= 8)
		{
			return Calendar.TimeOfDay.DAY;
		}
		if (hourInt >= 20 || hourInt < 7)
		{
			return Calendar.TimeOfDay.NIGHT;
		}
		if (hourInt >= 7 && hourInt < 8)
		{
			return Calendar.TimeOfDay.DAWN;
		}
		if (hourInt >= 19 && hourInt < 20)
		{
			return Calendar.TimeOfDay.DUSK;
		}
		return Calendar.TimeOfDay.DAY;
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x0003602C File Offset: 0x0003422C
	public static bool isItDay()
	{
		return Calendar.getTimeOfDay() == Calendar.TimeOfDay.DAY;
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00036039 File Offset: 0x00034239
	public static bool isItDawn()
	{
		return Calendar.getTimeOfDay() == Calendar.TimeOfDay.DAWN;
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00036046 File Offset: 0x00034246
	public static bool isItDusk()
	{
		return Calendar.getTimeOfDay() == Calendar.TimeOfDay.DUSK;
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00036053 File Offset: 0x00034253
	private static int parse(string input)
	{
		if (input == "")
		{
			return 0;
		}
		return int.Parse(input);
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x0003606A File Offset: 0x0003426A
	public static float getYearProgression()
	{
		return Calendar.getDayInt() / 390f;
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x00036079 File Offset: 0x00034279
	public static float getHourInMinuteProgression()
	{
		return Calendar.getMinutesInt() / 60f;
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x00036088 File Offset: 0x00034288
	public static float getSeasonalProgression()
	{
		return (Mathf.Cos(6.2831855f * Calendar.getYearProgression()) + 1f) / 2f;
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x000360A6 File Offset: 0x000342A6
	public static float getDayProgression()
	{
		return (float)Calendar.getHourInt() / 24f;
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x000360B4 File Offset: 0x000342B4
	public static ulong getSecond()
	{
		return Calendar.seconds;
	}

	// Token: 0x04000300 RID: 768
	private static ulong seconds;

	// Token: 0x04000301 RID: 769
	private static int year;

	// Token: 0x04000302 RID: 770
	private const int SECONDS_IN_MINUTE = 60;

	// Token: 0x04000303 RID: 771
	private const int MINUTES_IN_HOUR = 60;

	// Token: 0x04000304 RID: 772
	private const int HOURS_IN_DAY = 24;

	// Token: 0x04000305 RID: 773
	private const int DAYS_IN_MONTH = 30;

	// Token: 0x04000306 RID: 774
	private const int MONTHS_IN_YEAR = 13;

	// Token: 0x04000307 RID: 775
	private const int DAYS_IN_YEAR = 390;

	// Token: 0x04000308 RID: 776
	private const uint SECONDS_IN_YEAR = 33696000U;

	// Token: 0x04000309 RID: 777
	private static ulong startDay;

	// Token: 0x0200023F RID: 575
	private enum TimeOfDay
	{
		// Token: 0x040008B3 RID: 2227
		NIGHT,
		// Token: 0x040008B4 RID: 2228
		DAWN,
		// Token: 0x040008B5 RID: 2229
		DAY,
		// Token: 0x040008B6 RID: 2230
		DUSK
	}
}
