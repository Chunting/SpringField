====================================
windows service 版本
使用前在 SDK command prompt中用installutil命令安装 （卸载命令是installutil /u）
sample: 如果编译好的windows service程序 myservice.exe 在c:\\myservice\bin\debug\myservice.exe
        安装命令为    installutil c:\\myservice\bin\debug\myservice.exe
        卸载命令为    installutil /u c:\\myservice\bin\debug\myservice.exe
        当然也可以进入程序所在目录后直接安装该文件， 如 cd c:\\myservice\bin\debug
														installutil myservice.exe

默认手动状态，使用前需要启动
log文件为windows\system32目录下mailSender.log//LMM:log在配置文件里自定义

=====================================
v3
1. 增加log文件用于记录程序的所有事件
2. 对可能出现的异常作出处理
   规范的mail格式中“from”有且只有一个合法地址，“to”至少有一个地址，且每个地址必须合法，“cc”和“bcc”可以为空，但地址必须合法。
 不符合上述mail格式的记录将引发异常，log文件中记录为“Invalid mail address found”

==================================
mailsender包括两个主要的流程：
（1）检查并发送数据库中尚未发送的邮件
（2）检查DueDate在两天之内，尚未完成的interview。给interviewer和hiringmanager发送提醒信

程序启动后，两个流程分别按照各自的周期运行。
缺省设置：流程（1）每小时检查一次，程序启动时第一次检查
           流程（2）每天00：00检查一次

更改周期方法：
/MailSender/App.config文件
键值checkUnsentMailInterval是流程（1）的周期，格式：  day:hour:minute:second
键值checkUnfinishedInterviewInterval是流程（2）的周期，格式同上
键值checkUnfinishedInterviewDelay是流程（3）的第一次启动时间到程序启动时间的延时，格式同上（设置为0:0:0:0，程序运行时即进行流程2）

键值为空时采用缺省设置
======================================