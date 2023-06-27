using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iWareDao.Entity
{
    /// <summary>
    /// 加工参数
    /// </summary>
    [Table("machining")]
    [Comment("加工参数表")]
    public class Machining:BaseEntity
    {
        /// <summary>
        /// 毛坯站视觉移栽伺服当前速度
        /// </summary>
        [Comment("1")]
        public float M_DBD60 { get; set; }

        /// <summary>
        /// 毛坯站视觉移栽伺服设定速度
        /// </summary>
        public float M_DBD64 { get; set; }

        /// <summary>
        /// 毛坯站视觉移栽伺服当前位置
        /// </summary>
        public float M_DBD68 { get; set; }

        /// <summary>
        /// 毛坯站视觉移栽伺服故障代码 为0时无故障
        /// </summary>
        public short M_DBW72 { get; set; }

        /// <summary>
        /// 毛坯站移栽取料伺服当前速度
        /// </summary>
        public float M_DBD74 { get; set; }

        /// <summary>
        /// 毛坯站移栽取料伺服设定速度
        /// </summary>
        public float M_DBD78 { get; set; }

        /// <summary>
        /// 毛坯站移栽取料伺服当前位置
        /// </summary>
        public float M_DBD82 { get; set; }

        /// <summary>
        /// 毛坯站移栽取料伺服故障代码 为0时无故障
        /// </summary>
        public short M_DBW86 { get; set; }

        /// <summary>
        /// 毛坯站角向夹紧伺服当前速度
        /// </summary>
        public float M_DBD90 { get; set; }

        /// <summary>
        /// 毛坯站角向夹紧伺服设定速度
        /// </summary>
        public float M_DBD94 { get; set; }

        /// <summary>
        /// 毛坯站角向夹紧伺服当前位置
        /// </summary>
        public float M_DBD98 { get; set; }

        /// <summary>
        /// 毛坯站角向夹紧伺服故障代码 为0时无故障
        /// </summary>
        public short M_DBW102 { get; set; }

        /// <summary>
        /// 毛坯站角向旋转伺服当前速度
        /// </summary>
        public float M_DBD104 { get; set; }

        /// <summary>
        /// 毛坯站角向旋转伺服设定速度
        /// </summary>
        public float M_DBD108 { get; set; }

        /// <summary>
        /// 毛坯站角向旋转伺服故障代码
        /// 16#7002为轴激活
        /// </summary>
        public short M_DBD112 { get; set; }

        /// <summary>
        /// 毛坯站工件加工程序
        /// 1代表6A1机种，2代表6MB机种，3暂定为5AF机种
        /// </summary>
        public int M_DBW114 { get; set; }

        /// <summary>
        /// 毛坯站上料节拍
        /// 32位数据 单位秒
        /// </summary>
        public int M_DBD116 { get; set; }

        /// <summary>
        /// 毛坯站角向当前读码内容
        /// 30个字符
        /// </summary>
        public string? M_DBB120 { get; set; }

        /// <summary>
        /// 抽检台OP10当前扫码内容
        /// 30位字符
        /// </summary>
        public string? M_DBB150 { get; set; }

        /// <summary>
        /// 2转4暂存料道下料伺服当前速度
        /// </summary>
        public float M_DBD180 { get; set; }

        /// <summary>
        /// 2转4暂存料道下料伺服设定速度
        /// </summary>
        public float M_DBD184 { get; set; }

        /// <summary>
        /// 2转4暂存料道下料伺服当前位置
        /// </summary>
        public float M_DBD188 { get; set; }

        /// <summary>
        /// 2转4暂存料道角向夹紧伺服故障代码
        /// 为0时无故障
        /// </summary>
        public short M_DBW192 { get; set; }

        /// <summary>
        /// 2转4暂存料道上料伺服当前速度
        /// </summary>
        public float M_DBD194 { get; set; }

        /// <summary>
        /// 2转4暂存料道上料伺服设定速度
        /// </summary>
        public float M_DBD198 { get; set; }

        /// <summary>
        /// 2转4暂存料道上料伺服当前位置
        /// </summary>
        public float M_DBD202 { get; set; }

        /// <summary>
        /// 2转4暂存料道上料伺服故障代码
        /// </summary>
        public short M_DBW206 { get; set; }

        /// <summary>
        /// 2转4暂存料道角向夹紧伺服当前速度
        /// </summary>
        public float M_DBW208 { get; set; }

        /// <summary>
        /// 2转4暂存料道角向夹紧伺服设定速度
        /// </summary>
        public float M_DBD212 { get; set; }

        /// <summary>
        /// 2转4暂存料道角向夹紧伺服当前位置
        /// </summary>
        public float M_DBD216 { get; set; }

        /// <summary>
        /// 2转4暂存料道角向夹紧伺服故障代码
        /// </summary>
        public short  M_DBD220 { get; set; }

        /// <summary>
        /// 2转4暂存料道角向旋转伺服当前速度
        /// </summary>
        public float M_DBD222 { get; set; }

        /// <summary>
        /// 2转4暂存料道角向旋转伺服设定速度
        /// </summary>
        public float M_DBD226 { get; set; }

        /// <summary>
        /// 2转4暂存料道角向旋转伺服故障代码
        /// 16#7002为轴激活
        /// </summary>
        public short M_DBD230 { get; set; }

        /// <summary>
        /// 2转4暂存料道机种
        /// 1代表6A1机种，2代表6MB机种，3暂定为5AF机种
        /// </summary>
        public int M_DBW232 { get; set; }

        /// <summary>
        /// 2转4暂存料道上料节拍
        /// 32位数据    单位为秒
        /// </summary>
        public int M_DBD234 { get; set; }

        /// <summary>
        /// 2转4暂存料道角向当前读码
        /// 30个字符
        /// </summary>
        public string? M_DBB238 { get; set; }
    }
}
