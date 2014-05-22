using UnityEngine;
using System.Collections;

public class GeneticGenerator : MonoBehaviour {

	void Start () {
	
	}
	

	void Update () {
	
	}


/*import bpy
#from math import cos, sin, tan, acos, asin, atan, pi, sqrt
from bpy.props import *
from random import randint, random, uniform*/

// variable globale, contenant la population du problème
List<Plant> population;


# classe définissant les individus génériques d'une population
class Plant():
    def __init__(self, _attributes):
        self.attributes = []
        for a in _attributes:
            self.attributes.append(a)
        self.fitness = 0
    
    def __repr__(self):
        string = ''
        for a in self.attributes:
            string += str(a)+'\n'
        return string



#fonctions utilisées dans l'algorithme génétique
def generateRandomPopulation(populationSize,numberOfAttributes,sequenceLength):
    population.clear()
    for i in range(populationSize):
        attributes = []
        for j in range(numberOfAttributes):
            newAttribute = []  
            for k in range(sequenceLength):
                newAttribute.append(randint(0,1))
            attributes.append(newAttribute)
        newIndividual = individual(attributes)  
        population.append(newIndividual)
                
def printPopulation():
    for i in population:
        print(i)
    print('\n\n')
    
# fonction d'évaluation d'un individu (exemple faisant la somme des valeurs des attributs)    
def evaluateFitness(individual):
    
    individual.fitness = 0
    for a in individual.attributes:
        attributeValue = 0
        reversedAttribute = reversed(a)
        index = 0
        for bit in reversedAttribute :
            if bit==1: attributeValue += pow(2,index)
            index+=1
        individual.fitness += attributeValue
    
    return individual.fitness    
        

# sélection des parents (doit etre utilisée sur une population de taille paire)
# requierant un parametre compris entre 0.5 et 1.0
def selectParents(elitism):
    populationSize=len(population)
    newParents = []
    while (len(population)>populationSize/2):
        index1=randint(0,len(population)-1)
        index2=randint(0,len(population)-1)
        while (index2==index1) : index2=randint(0,len(population)-1)
        if(index1==index2):print('Attention : meme parent choisi 2 fois !')
        
        if (population[index1].fitness < population[index2].fitness):
            population[index1],population[index2] = population[index2],population[index1]
        # on est donc maintenant sûr que l'individu le mieux adapté est à la position index
        r = random()
        if (r<elitism):
            newParents.append(population[index1])
            population.pop(index1)
        else :
            newParents.append(population[index2])
            population.pop(index2)
    return newParents
        

def mutatePopulation(_population,mutationProbability):
    for i in _population:
        for j in i.attributes:
            r = random()
            if (r < mutationProbability):
                #print('Mutation :\n')
                #print('   ' + str(i))
                index = randint(0,len(j)-1)
                if (j[index]==0) : j[index]=1
                else : j[index]=0
                #print('   ' + str(i))

# croisement entre 2 individus, avec un seul point de croisement, à la position splitIndex
def crossIndividuals(indiv1, indiv2,splitIndex):
    child1Attributes = []
    child2Attributes = []
    
    for i in range(len(indiv1.attributes)):
        child1NewAttribute = crossSequences(indiv1.attributes[i],indiv2.attributes[i],splitIndex)
        child1Attributes.append(child1NewAttribute)
        child2NewAttribute = crossSequences(indiv2.attributes[i],indiv1.attributes[i],splitIndex)
        child2Attributes.append(child2NewAttribute)
    
    child1 = individual(child1Attributes)
    child2 = individual(child2Attributes)
    return child1,child2
    
    
def crossSequences(seq1,seq2,splitIndex):
    newSequence=[]
    for i in range(len(seq1)):
        if(i<splitIndex): newSequence.append(seq1[i])
        else: newSequence.append(seq2[i])
    return newSequence
    
    
    
########## BOUCLE PRINCIPALE #############    
    
populationSize = 20     # taille de la population
numberOfAttributes = 4    # nombre de chromosomes par individu
sequenceLength = 10     # longueur des chromosomes
mutationProbability = 0.05   # probabilité de mutation 
elitism = 0.75     # probabilité que le vainqueur d'un affrontement soit le plus adapté des deux

print('!!!!!! NOUVELLE PARTIE !!!!!!')

generateRandomPopulation(populationSize,numberOfAttributes,sequenceLength)
nbOfTurn = 0

avgFitness = 0  # coefficient d'adaptation moyen de la population
maxFitness = 0 # coefficient d'adaptation maximal
winner = None # individu ayant ce coefficient maximal

while (maxFitness<3700):
    nbOfTurn += 1
    maxFitness = 0
    sumFitness = 0
    for i in population:
        evaluateFitness(i)
        sumFitness += i.fitness
        if (i.fitness>maxFitness):
            maxFitness=i.fitness
            winner = i
    avgFitness = sumFitness/len(population) 
    
    print ('#' + str(nbOfTurn))
    print ('maximum fitness = ' + str(maxFitness) ) #+ '   average fitness = ' + str(avgFitness) )     
    
    # selection des parents par tournois succesifs parmi la population
    parents = selectParents(elitism)
    temporaryParents = parents # copie de la liste utilisée pour la condition d'arrêt de la boucle while qui suit
    
    children = [] 
    while (len(temporaryParents)>0) :
        index1 = randint(0,len(temporaryParents)-1)
        index2 = randint(0,len(temporaryParents)-1)
        while (index2==index1) : index2=randint(0,len(temporaryParents)-1)
        splitIndex = randint(0,sequenceLength-1)
        child1,child2 = crossIndividuals(temporaryParents[index1],temporaryParents[index2],splitIndex)
        children.append(child1)
        children.append(child2)
        if (index1>index2): index1,index2 = index2,index1
        temporaryParents.pop(index2)
        temporaryParents.pop(index1)
        #print('Il reste ' + str(len(parents)) + ' parents')
    
    # Seuls les enfants ont une probabilité de muter    
    mutatePopulation(children,mutationProbability)
    
    # On peut choisir le pourcentage de parents qui seront remplacés par leurs enfants.
    for c in children:
        population.append(c)
        
print('le gagnant est : \n' + str(winner))

}
