# etc
육아 진행중이라 작업이 자주 진행이 안됩니다.

# portfolio-rest-api
로그라이크성 게임.

인증용 MemberDb, 공용 GlobalDb, 서버당 샤딩한 GameDbXX 사용

Request / Response protobuf 사용. 

서버당 캐릭터 하나 보유.

캐릭터 레벨, 직업, 스킬, 아이템 성장.

캐릭터 사망시 보유 능력 (스킬, 아이템, 레벨 등) 중 하나 다음 캐릭터에 상속 후 삭제

메인 게임은 던전 돌파 / 핵앤슬래시로?

다양한 직업을 준비, 3~5단계의 직업 전직. 직업에 따른 차별적인 스킬. 하나의 직업당 2개의 스킬 습득. 습득한 스킬에 따른 시너지.

직업 + 아이템 + 스킬로 인한 차별적인 플레이

인게임 진행은 이벤트 소싱으로 저장 및 필요시 검증 | 로드

LoginServer
 - 유저 인증
 - 유저 검증

GameServer
 - 서버 조회
 - 캐릭터 조회 / 생성
 - 데이터 조회(미작업)
 - 출석 / 이벤트(미작업)
 - Behavior 처리(미작업)
 - 인게임 진행(미작업)
 - 강화(미작업)
