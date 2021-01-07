pipeline {
	agent {
		docker { image 'mcr.microsoft.com/dotnet/core/sdk:3.1' }
	}
	stages {
		stage('pull') {
			steps {
				git(url: 'https://github.com/cristianeph/netcore-api-test.git', credentialsId: 'github')
			}
		}
		stage('build') {
			steps {
				sh 'ls && dotnet publish && ls'
			}
		}
	}
}