﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SVGImage.SVG
{
    public static class StyleParser
    {
        private static Regex _regexStyle =
            new Regex("([\\.,<>a-zA-Z0-9: \\-#]*){([^}]*)}", RegexOptions.Compiled | RegexOptions.Singleline);

        public static void ParseStyle(SVG svg, string style)
        {
            var match = _regexStyle.Match(style);
            while (match.Success)
            {
                var name = match.Groups[1].Value.Trim();
                var value = match.Groups[2].Value.Trim();
                foreach (var nm in name.Split(','))
                {
                    svg.m_styles.Add(nm, new List<XmlUtil.StyleItem>());
                    foreach (ShapeUtil.Attribute styleitem in XmlUtil.SplitStyle(svg, value))
                    {
                        svg.m_styles[nm].Add(new XmlUtil.StyleItem(styleitem.Name, styleitem.Value));
                    }
                }

                match = match.NextMatch();
            }
        }
    }
}
