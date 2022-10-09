//пришло сообщение
//поднялся стейт саги из бд
//создался объект процесс манагера
//--десериловался стейт саги в граф
//создался стейт машин визитор для сбора стейт машины по графу
//запускаем визитора
//создался рейз контекст с объектом процесс манагера
//в контекст забросился эвент, там этот эвент привел к поведению, которое определило следующие сообщения, либо бросило ошибку, либо еще что-то
//запрашиваем следующие сообщения - подготавливаем к отправке, если все упало, то обрабатываем как-то ошибку (даже системную)
//создался стейт машин визитор для сбора графа по стейт машине
//запускаем визитора
//собираем граф, персистим

//state lifetime:
//1. on enter state - Enter event is raised when all enter prerequisites became resolved. Enter event can launch on enter behaviors
//2. during state - reacts on events raised in this state and launches activity behaviors
//3. on leave state - all state dependencies were completed, reacts on Leave raised event transition behavior with transition activities

//it's possible to have many inputs but must have one out in graph
//it's possible to have transition to the same state

//save and restore visitor (+)
//PropagateEventAsync копипаста здесь
//FinishProcess, а если не указать?
//ошибки ProcessManagerException
//tests (mb спекфлоу)
//performance benchmarks
//sensors
//adaptation to bpmn
//extra pm store

//addtional features:
//how to receive message with the same type but FOR different states?
//event stream
//independent events/transitions
//recursive transition
//versioning
//from dot graph to models
//aop, logging aspect instead of proxies (ingest IL to methods)
//subprocesses

//1. стейт машина в каком-то стейте
//2. в нее влетает эвент
//3. может произойти следующее:
//-успешное потребление эвента стейтами
//-рейз новых эвентов
//-неуспешное потребление эвента (выброс в помойку)
//-реакция стейта на эвент и вызов определенного behavior chain
//-публикация Outgoing candidates
//-аборт процесса
//-старт процесса
//-финиш процесса

+ везде должен быть вездесущий save/restore

//я перешел в стейт
//если пришло сообщение А, то идем в ветку А
//или если пришло сообщение Б, то идем в ветку Б

//я перешел в стейт
//если пришло сообщение А, то идем в ветку А и в ветку Б

//не поддерживается ядром, как реализовать? я перешел в стейты и там уже слушаю сообщения
//я перешел в стейт
//если пришло сообщение А, то идем в ветку А
//потом если пришло сообщение Б, то идем в ветку Б







-------------------------------------------------------------

//что если произошел error внутри activity - после любой активити идет EventBased XOR гейтвей
//где 1 эвент - это none (типо все ок), 2 эвент - это ErrorEvent, как потом кетчить этот гейтвей?

//что если какой-то интераптинг аттачед эвент произошел во время выполнения активити
//что если какой-то не интераптинг аттченд эвент произошел во время выполнения активити
//что такое таск?
//как отработает termnation event

//все данные для выполнения едниницы работы должны явным образом приходить в компонент
//у компонента нет доступа к глобальному контексту данных

//динамические ветви-процессы, например на каком-то шаге после выполнения работы появляется айтем у которого создается своя ветвь с действиями


1. process, process with result
2. DSL
3. process manager, process storage, message sender, contexts
4. DSL
5. implement some units

- connections
- events:
   - catching message
   - throwing message
   - empty
   - error
   - termination
- gateways:
   - parallel, exclusive, event based
- tasks
   - action task
   - action task with input
   - action task with input and output
   - action task with output
   - async action task
   - async action task with input
   - async action task with input and output
   - async action task with output
   - ...

6. proxy, factories
8. basic test playground
9. graph, visitors
10. sensors
11. test specs