# 전국퇴마사협회(Korean Exorcist Association)
 ![image](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/5e55844a-a641-433c-b994-b7e760234af5)
<br>

2023 광주글로벌게임센터 인디스타즈(INDISTARS) 8기 프로젝트 

🎥 데모 영상 : https://www.youtube.com/watch?v=ZtUz-zlxgwA

## 📝 프로젝트  개요

팀 명 : 팀 다다

프로젝트 명 : 전국퇴마사협회(Korean Exorcist Association)

게임 플랫폼 : PC

게임 장르 : 현대 한국오컬트 판타지 추리 조사 RPG

⚙️ 개발 환경
- `Unity 2D`

🕰️ 프로젝트 기간 : 2023년 5월 ~ 2023년 12월

<br>

## 🖥️ 프로젝트 소개
어머니에게 물려받은 영능력으로 인해 여러 사건사고가 끊이지 않는 귀신을 보는 소녀 혜성. 그러던 와중 우연한 계기로 전국퇴마사협회와 엮인 혜성이 동료 퇴마사들과 힘을 합쳐 악귀의 발생 원인을 추리하고 악귀가 나오는 결계를 봉인하는 한국 오컬트 소재의 RPG 게임

▷  당신은...

● 무녀였던 어머니의 영향을 받아 종종 보지 말아야 할 것들을 보는 소녀 혜성입니다.

● 언제나와 같은 하굣길, 혜성은 우연한 계기로 현실이 아닌 다른 공간에 들어가게 됩니다. 그곳에서 악의를 가지고 있는 악귀와 마주하고, 마침 만나게 된 다른 퇴마사에 의해 전국퇴마사협회를 알게 됩니다. 

▷  악귀, 그리고 그 주변의 이야기...

● 전국퇴마사협회 퇴마사의 일원으로써 악귀를 퇴마하기 위해서는 죽음에 대한 넋을 달래야 합니다.

● 악귀가 죽어서도 남아있게 만드는 한의 원인을 주변 탐문을 통해 밝히고, 악귀에게 이어진 이야기에 집중하세요. 어쩌면 뉴스에서 자주 접한 이야기의 주인공이, 악귀일 수도 있습니다.

▷  진실과 퇴마

● 악귀에 대한 진실에 근접하고 악귀가 몸을 담고 있는 결계에 입장하여 악귀와 최후의 전투를 치릅니다.

● 결계 안의 잡귀와 전투하고, 결계 안에 악귀의 심상이 반영된 공간을 지나 끝내 악귀와 마주보세요.

<br>

## 📌 프로젝트 기획의도
 ❍ 무섭거나 공포스럽게 다루어졌던 귀신이나 죽음에 대한 소재에 이야기를 부여하여 기존의 부정적이라는 고정관념을 깨고 감동을 주고자 함. 
 
 ❍ 한국적 소재를 사용한 게임의 수가 적기에 이를 활용한 게임을 만들어 한국 소재에 대한 인지도를 높이고자 함.
 
 ❍ 오컬트라는 재미있고 독특한 소재를 바탕으로 지금까지의 준비를 바탕으로 한 시도되지 못한 추리와 조사 게임을 만들어보고자 함.

 ❍ '퇴마' 한다는 설정을 이용하여 지금까지는 없었던, 추리와 전투의 조합의 게임 시스템 도전

<br>

## 🙋 프로젝트 팀원 구성
총 5명(프로그래밍2 & 디자인3)
- 양성아(팀장) : 시스템 기획 및 스토리 검수, 서브 프로그래밍(전투, UI)
- 김경태(개발) : 메인 프로그래밍(전반적인 모든 프로그래밍 역할 수행)
- 김나현(디자인) : 디자인 기획 및 스토리 작성, 오브젝트 및 맵 디자인
- 나유영(디자인) : UI 디자인, 더빙
- 정서연(디자인) : 캐릭터, 귀신 디자인 및 원화, 캐릭터 스탠딩

<br>

## 🤝 사용한 에셋들
- LeanTween : https://assetstore.unity.com/packages/tools/animation/leantween-3595
- Sound :

  https://assetstore.unity.com/packages/audio/music/electronic/dark-atmospheric-free-track-music-pack-adaptive-tracks-244634

  https://assetstore.unity.com/packages/audio/music/orchestral/the-fantasy-music-collection-starter-15901

<br>

## 🔎 맡은 구현 부분들

<details>
<summary><h4>대화 시스템</h4></summary>

 ![image](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/91f53798-43fe-4d04-aa47-3f7b55bd6510)

1. CSV 대화 스크립트 파일 데이터를 파싱하여 대화 시스템 구축

 ![image](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/7b99e77f-1d0c-4d83-9a7c-604bdbfb667a)

2. 원활한 대화 출력을 위해 긴 대화는 대화 2줄로 이어지도록 한다.

3. 대화하는 사람에 따른 상태(Sprite) 변화 적용.

4. 대화 Skip 버튼 구현 및 대화하는 사람의 이름표가 보여진다.
</details>

<details>
<summary><h4>조사 시스템</h4></summary>

 ![image](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/d2ca8f1e-6464-40a8-ab81-8dde3cf31205)

1. 마우스 포인터가 닿았을 때 Hover 이미지 반영

 ![image](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/cb2e3bb5-461f-472d-bba5-5de45aa1a9e9)

2. 단서를 조사할 시 백그라운드 이미지 반영

![image](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/c7a5336a-3539-4584-812d-90e132424160)

3. 조사할 맵(씬)에 입장 시 방문 카운트가 증가한다.
   
![ezgif com-video-to-gif-converted (5)](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/a6754d7d-ce8a-42b6-93f2-c631fdadbc10)

4. 모든 맵을 조사할 때 중앙 결계가 Open 되도록 한다.
</details>

<details>
<summary><h4>인벤토리 시스템</h4></summary>
 
 ![image](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/4b38bb29-9500-46f0-8aa2-1cfbf75265be)

1. 조사한 단서는 인벤토리에 추가 된다.
 
2. 한 번 추가한 단서는 중복으로 반영되지 않는다.

3. 조사한 단서를 클릭 시 해당 단서에 대한 설명을 보여준다.

4. 씬을 이동해도 인벤토리 내 단서는 유지된다.

5. 게임을 재시작 시 인벤토리 내 단서는 초기화된다.
</details>

<details>
<summary><h4>추리 시스템</h4></summary>
 
 ![image](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/e440f0b6-859c-414e-9824-b95425518869)

1. 사용자는 각 맵에서 조사 진행을 마치고, 악귀에 대한 한을 추리한다.
 
2. 질문에 대한 각 선택지 정보는 JSON 파일로 관리한다.
   
![ezgif com-video-to-gif-converted](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/176fb6ef-0f96-4ac1-b91f-77ebb9513e6e)

3. 조사한 단서를 클릭 시 해당 단서에 대한 설명을 보여준다.<br> 질문에 대한 선택지가 각각 다르게 존재하고, 각 선택지를 클릭할 때 색상, 텍스트 변화를 확인할 수 있다.

4. 각 질문에 대한 선택을 확정할 때 답안지에 기록이 된다.

5. 확정한 질문 탭은 다시 이동하여 수정할 수 없다.

6. 추리에 대한 정답 개수는 이후 게임을 마친 뒤에 성적표 반영에 영향을 끼치게 된다.
</details>

<details>
<summary><h4>기타</h4></summary>
 
 ![ezgif com-video-to-gif-converted (1)](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/b50d293a-10fc-485c-955a-458e7402953f)
 ![ezgif com-video-to-gif-converted (6)](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/1fa85ae6-b520-4a30-8137-b9e498211248)
 ![ezgif com-video-to-gif-converted (7)](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/7b0da59a-2ded-4825-b5bc-0bfa4feb3f12)

1. Lean Tween 외부 에셋을 이용하여 대화 중 자연스러운 컷신 변환

 ![ezgif com-video-to-gif-converted (3)](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/99853b86-b5a5-42b3-98ae-b4d0f6b815e5)

2. Lean Tween 외부 에셋을 이용하여 전투 중 캐릭터 선택에 대한 자연스러운 애니메이션 연출

![ezgif com-video-to-gif-converted (4)](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/b9f1f4ff-53d5-4ef9-94ed-1aa356d46c2d)

3. 페이드 인 아웃 효과를 부여하여 전반적인 씬 관리를 구현 

![ezgif com-speed](https://github.com/KimGyoungTae/Public_TheAssociation_of_Exorcists/assets/83820089/3f03bdd3-d47b-4b1c-ba2f-38d65648312d)

4. 추리 시스템에서의 정답 개수를 기반으로 성적표 반영 구축
5. 프롤로그 영상 및 씬에 따라 다른 배경음악 구축.
</details>
 








