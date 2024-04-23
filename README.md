![image](https://github.com/RafaelaNasciment/StandardProject/assets/101521570/a67163ce-3c56-4c83-af86-ec12f84835ca)


Arquitetura em camadas ou arquitetura em camadas limpas (Clean Architecture)

Apresentacao 

Camada é responsável por lidar com as solicitações HTTP, roteamento e apresentação dos dados. Os controllers devem ser responsáveis por receber as requisições, chamar os serviços apropriados e retornar as respostas. Parece que você está seguindo essa abordagem.

Domain
Camada contém as entidades de negócio e as regras de negócio básicas. É importante manter esta camada desacoplada das camadas superiores, como a de apresentação e serviço

Service
Esta camada é responsável por conter a lógica de negócio da sua aplicação. Ela coordena as operações entre as entidades de domínio e a camada de infraestrutura. É uma boa prática manter essa camada magra, delegando a maior parte da lógica para classes específicas de domínio quando possível.

Conexão domain e infra

Repository

Camada de acesso a dados, que se comunica com o banco de dados. É comum ter interfaces definindo os contratos para operações de acesso a dados e implementações concretas dessas interfaces para interagir com o banco de dados. Essa separação permite fácil substituição de implementações e facilita os testes unitários.

Infrastructure

Camada é responsável por configurações do projeto, conexões externas e configurações globais. Isso inclui configurações de banco de dados, configurações de logging, injeção de dependência, etc.

Testes 

Camada de testes unitários para a camada da aplicação, visando compromisso com a qualidade do código.
