====================================
windows service �汾
ʹ��ǰ�� SDK command prompt����installutil���װ ��ж��������installutil /u��
sample: �������õ�windows service���� myservice.exe ��c:\\myservice\bin\debug\myservice.exe
        ��װ����Ϊ    installutil c:\\myservice\bin\debug\myservice.exe
        ж������Ϊ    installutil /u c:\\myservice\bin\debug\myservice.exe
        ��ȻҲ���Խ����������Ŀ¼��ֱ�Ӱ�װ���ļ��� �� cd c:\\myservice\bin\debug
														installutil myservice.exe

Ĭ���ֶ�״̬��ʹ��ǰ��Ҫ����
log�ļ�Ϊwindows\system32Ŀ¼��mailSender.log//LMM:log�������ļ����Զ���

=====================================
v3
1. ����log�ļ����ڼ�¼����������¼�
2. �Կ��ܳ��ֵ��쳣��������
   �淶��mail��ʽ�С�from������ֻ��һ���Ϸ���ַ����to��������һ����ַ����ÿ����ַ����Ϸ�����cc���͡�bcc������Ϊ�գ�����ַ����Ϸ���
 ����������mail��ʽ�ļ�¼�������쳣��log�ļ��м�¼Ϊ��Invalid mail address found��

==================================
mailsender����������Ҫ�����̣�
��1����鲢�������ݿ�����δ���͵��ʼ�
��2�����DueDate������֮�ڣ���δ��ɵ�interview����interviewer��hiringmanager����������

�����������������̷ֱ��ո��Ե��������С�
ȱʡ���ã����̣�1��ÿСʱ���һ�Σ���������ʱ��һ�μ��
           ���̣�2��ÿ��00��00���һ��

�������ڷ�����
/MailSender/App.config�ļ�
��ֵcheckUnsentMailInterval�����̣�1�������ڣ���ʽ��  day:hour:minute:second
��ֵcheckUnfinishedInterviewInterval�����̣�2�������ڣ���ʽͬ��
��ֵcheckUnfinishedInterviewDelay�����̣�3���ĵ�һ������ʱ�䵽��������ʱ�����ʱ����ʽͬ�ϣ�����Ϊ0:0:0:0����������ʱ����������2��

��ֵΪ��ʱ����ȱʡ����
======================================